using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using VideoPlatform.Common.Infrastructure.Helpers;
using VideoPlatform.Common.Models.Enums;
using VideoPlatform.DAL.Interfaces;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public abstract class CosmosEntityRepository<TEntity> : ICosmosEntityRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    private readonly Container _container;

    protected CosmosEntityRepository(CosmosContext dbContext, string containerName)
    {
        if (dbContext?.CosmosDatabaseResponse?.Database != null)
        {
            var containerResponse = AsyncHelper.RunSync(async () =>
                await dbContext.CosmosDatabaseResponse.Database.CreateContainerIfNotExistsAsync(
                    new ContainerProperties
                    {
                        Id = containerName,
                        IndexingPolicy = { Automatic = true, IndexingMode = IndexingMode.Consistent },
                        PartitionKeyPath = "/id"
                    }));

            _container = containerResponse.Container;
        }
        else
        {
            throw new ArgumentNullException(nameof(dbContext));
        }
    }

    public async Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _container.ReadItemAsync<TEntity>(id.ToString(), new PartitionKey(id.ToString()), null,
            cancellationToken);
        return result.Resource;
    }

    public async Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default)
    {
        var result = filterExpression != null
            ? await _container.GetItemLinqQueryable<TEntity>().Where(filterExpression).ToFeedIterator()
                .ReadNextAsync(cancellationToken)
            : await _container.GetItemLinqQueryable<TEntity>().ToFeedIterator().ReadNextAsync(cancellationToken);
        return result?.FirstOrDefault();
    }

    public async Task<ICollection<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default)
    {
        var result = filterExpression != null
            ? await _container.GetItemLinqQueryable<TEntity>().Where(filterExpression).ToFeedIterator()
                .ReadNextAsync(cancellationToken)
            : await _container.GetItemLinqQueryable<TEntity>().ToFeedIterator().ReadNextAsync(cancellationToken);
        return result?.ToList();
    }

    public async Task<PagingResult<TEntity>> GetPagingEntitiesAsync(Paging<TEntity> pagingModel,
        CancellationToken cancellationToken = default)
    {
        var query = _container.GetItemLinqQueryable<TEntity>();
        if (pagingModel.FilterExpression != null)
            query = (IOrderedQueryable<TEntity>)query.Where(pagingModel.FilterExpression);

        query = pagingModel.SortOrder switch
        {
            SortOrder.None => query.OrderBy(x => x.Id),
            SortOrder.Ascending => query.OrderBy(pagingModel.SortedProperty),
            SortOrder.Descending => query.OrderByDescending(pagingModel.SortedProperty),
            _ => query
        };

        var result = await query.Skip((pagingModel.PageNumber - 1) * pagingModel.PageSize)
            .Take(pagingModel.PageSize).ToFeedIterator().ReadNextAsync(cancellationToken);

        return new PagingResult<TEntity>
        {
            Items = result?.ToList(),
            TotalCount = await query.CountAsync(cancellationToken)
        };
    }

    public async Task<bool> IsEntityExistAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = await _container.ReadItemAsync<TEntity>(entity.Id.ToString(),
            new PartitionKey(entity.Id.ToString()), null, cancellationToken);
        return result.Resource != null;
    }

    public async Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return await _container.CreateItemAsync(entity, new PartitionKey(entity.Id.ToString()), null,
            cancellationToken);
    }

    public async Task<IList<TEntity>> CreateEntitiesAsync(IList<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var firstEntity = entities.FirstOrDefault();
        if (firstEntity != null)
        {
            var transactionalBatch =
                _container.CreateTransactionalBatch(
                    new PartitionKey($"create{firstEntity.GetType()}_{firstEntity.Id}"));
            foreach (var entity in entities)
                transactionalBatch.CreateItem(entity);

            await transactionalBatch.ExecuteAsync(cancellationToken);
        }

        return entities;
    }

    public async Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _container.UpsertItemAsync(entity, new PartitionKey(entity.Id.ToString()), null, cancellationToken);
    }

    public async Task UpdateEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var firstEntity = entities.FirstOrDefault();
        if (firstEntity != null)
        {
            var transactionalBatch =
                _container.CreateTransactionalBatch(
                    new PartitionKey($"update{firstEntity.GetType()}_{firstEntity.Id}"));
            foreach (var entity in entities)
                transactionalBatch.UpsertItem(entity);

            await transactionalBatch.ExecuteAsync(cancellationToken);
        }
    }

    public async Task RemoveEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _container.DeleteItemAsync<TEntity>(id.ToString(), new PartitionKey(id.ToString()), null,
            cancellationToken);
    }

    public async Task RemoveEntitiesAsync(IList<Guid> ids, CancellationToken cancellationToken = default)
    {
        var firstId = ids.FirstOrDefault(x => !x.Equals(Guid.Empty));
        if (firstId != Guid.Empty)
        {
            var transactionalBatch = _container.CreateTransactionalBatch(new PartitionKey($"removeEntity_{firstId}"));
            foreach (var id in ids)
                transactionalBatch.DeleteItem(id.ToString());

            await transactionalBatch.ExecuteAsync(cancellationToken);
        }
    }
}