using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using VideoPlatform.Common.Models.Enums;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;
using IsolationLevel = System.Data.IsolationLevel;

namespace VideoPlatform.DAL.Repositories;

public abstract class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    private const int MaxReloadIteration = 10;

    protected EntityRepository(DbContext dbContext)
    {
        DatabaseContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    private DbContext DatabaseContext { get; }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        await DatabaseContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
    }

    public Task BeginTransaction()
    {
        DatabaseContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);

        return Task.CompletedTask;
    }

    public Task CommitTransaction()
    {
        DatabaseContext.Database.CommitTransaction();

        return Task.CompletedTask;
    }

    public Task RollbackTransaction()
    {
        DatabaseContext.Database.RollbackTransaction();

        return Task.CompletedTask;
    }

    public Task DisposeTransaction()
    {
        DatabaseContext.Database.CurrentTransaction?.Dispose();

        return Task.CompletedTask;
    }

    public async Task<TEntity> GetEntityByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await DatabaseContext.Set<TEntity>().AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default)
    {
        return filterExpression != null
            ? await DatabaseContext.Set<TEntity>().Where(filterExpression).AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken)
            : await DatabaseContext.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default)
    {
        return filterExpression != null
            ? await DatabaseContext.Set<TEntity>().Where(filterExpression).AsNoTracking().ToListAsync(cancellationToken)
            : await DatabaseContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PagingResult<TEntity>> GetPagingEntitiesAsync(Paging<TEntity> pagingModel,
        CancellationToken cancellationToken)
    {
        var query = DatabaseContext.Set<TEntity>().AsQueryable();
        if (pagingModel.FilterExpression != null)
            query = query.Where(pagingModel.FilterExpression);

        query = pagingModel.SortOrder switch
        {
            SortOrder.None => query.OrderBy(x => x.Id),
            SortOrder.Ascending => query.OrderBy(pagingModel.SortedProperty),
            SortOrder.Descending => query.OrderByDescending(pagingModel.SortedProperty),
            _ => query
        };

        return new PagingResult<TEntity>
        {
            Items = await query.Skip((pagingModel.PageNumber - 1) * pagingModel.PageSize).Take(pagingModel.PageSize)
                .AsNoTracking().ToListAsync(cancellationToken),
            TotalCount = await query.AsNoTracking().CountAsync(cancellationToken)
        };
    }

    public async Task<bool> IsEntityExistAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await DatabaseContext.Set<TEntity>().AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(entity.Id), cancellationToken) != null;
    }

    public async Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromMinutes(1)
        }, TransactionScopeAsyncFlowOption.Enabled);
        await DatabaseContext.AddAsync(entity, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        scope.Complete();

        return entity;
    }

    public async Task<IList<TEntity>> CreateEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken)
    {
        await using var transaction =
            await DatabaseContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        await DatabaseContext.BulkInsertAsync(entities, new BulkConfig
        {
            PreserveInsertOrder = false,
            SetOutputIdentity = true
        }, _ => { }, null, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return entities;
    }

    public async Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromMinutes(1)
        }, TransactionScopeAsyncFlowOption.Enabled);
        var dbEntity = await DatabaseContext.Set<TEntity>().AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(entity.Id), cancellationToken);
        if (dbEntity != null)
        {
            entity.RowVersion = dbEntity.RowVersion;
            DatabaseContext.Update(entity);

            var reloadIteration = 0;
            while (reloadIteration < MaxReloadIteration)
                try
                {
                    await DatabaseContext.SaveChangesAsync(cancellationToken);
                    reloadIteration = MaxReloadIteration;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await ex.Entries.Single().ReloadAsync(cancellationToken);
                    reloadIteration++;
                }
        }

        scope.Complete();
    }

    public async Task UpdateEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken)
    {
        var ids = entities.Select(x => x.Id).ToList();
        var dbEntities = await DatabaseContext.Set<TEntity>().AsNoTracking().Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        foreach (var entity in entities)
        {
            var dbEntity = dbEntities.SingleOrDefault(x => x.Id.Equals(entity.Id));
            if (dbEntity != null)
                entity.RowVersion = dbEntity.RowVersion;
        }

        await using var transaction =
            await DatabaseContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        await DatabaseContext.BulkUpdateAsync(entities, _ => { }, _ => { }, null, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RemoveEntityAsync(TKey id, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromMinutes(1)
        }, TransactionScopeAsyncFlowOption.Enabled);

        var entity = await DatabaseContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        if (entity != null)
        {
            DatabaseContext.Remove(entity);

            var reloadIteration = 0;
            while (reloadIteration < MaxReloadIteration)
                try
                {
                    await DatabaseContext.SaveChangesAsync(cancellationToken);
                    reloadIteration = MaxReloadIteration;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await ex.Entries.Single().ReloadAsync(cancellationToken);
                    reloadIteration++;
                }
        }

        scope.Complete();
    }

    public async Task RemoveEntitiesAsync(IList<TKey> ids, CancellationToken cancellationToken)
    {
        var entities = await DatabaseContext.Set<TEntity>().Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        await using var transaction =
            await DatabaseContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        await DatabaseContext.BulkDeleteAsync(entities, _ => { }, _ => { }, null, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}