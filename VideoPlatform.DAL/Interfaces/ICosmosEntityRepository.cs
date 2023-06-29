using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces;

public interface ICosmosEntityRepository<TEntity> where TEntity : BaseEntity<Guid>
{
    Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default);

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

    Task RemoveEntityAsync(Guid id, CancellationToken cancellationToken = default);

    Task RemoveEntitiesAsync(IList<Guid> ids, CancellationToken cancellationToken = default);
}