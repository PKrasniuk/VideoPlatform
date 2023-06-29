using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces;

public interface IEntityRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task BeginTransaction();

    Task CommitTransaction();

    Task RollbackTransaction();

    Task DisposeTransaction();

    Task<TEntity> GetEntityByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default);

    Task<ICollection<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> filterExpression = null,
        CancellationToken cancellationToken = default);

    Task<PagingResult<TEntity>> GetPagingEntitiesAsync(Paging<TEntity> pagingModel,
        CancellationToken cancellationToken = default);

    Task<bool> IsEntityExistAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IList<TEntity>> CreateEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    Task RemoveEntityAsync(TKey id, CancellationToken cancellationToken = default);

    Task RemoveEntitiesAsync(IList<TKey> ids, CancellationToken cancellationToken = default);

    //SomeHelp
    //public void Create(Guid key)
    //{
    //    _transactionProvider.BeginTransaction();

    //    try
    //    {
    //        _repo1.Create(key);
    //        _repo2.Create(key);
    //        _transactionProvider.CommitTransaction();
    //    }
    //    catch (Exception)
    //    {
    //        _transactionProvider.RollbackTransaction();
    //        throw;
    //    }
    //    finally
    //    {
    //        _transactionProvider.DisposeTransaction();
    //    }
    //}
}