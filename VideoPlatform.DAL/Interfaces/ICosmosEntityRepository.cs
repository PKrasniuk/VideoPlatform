using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.DAL.Models;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces
{
    public interface ICosmosEntityRepository<TEntity> where TEntity : BaseEntity<Guid>
    {
        Task<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filterExpression = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> filterExpression = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<PagingResult<TEntity>> GetPagingEntitiesAsync(Paging<TEntity> pagingModel, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> IsEntityExistAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<TEntity>> CreateEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateEntitiesAsync(IList<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveEntityAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveEntitiesAsync(IList<Guid> ids, CancellationToken cancellationToken = default(CancellationToken));
    }
}