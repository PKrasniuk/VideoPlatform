using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.ElasticSearchService.Models;

namespace VideoPlatform.ElasticSearchService.Interfaces
{
    public interface IIndexingEntityManager<TEntity>
    {
        Task IndexEntityAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveEntityAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task IsEntityExist(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task ReIndex(ICollection<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task<FilterResult<TEntity>> Find(Filter<TEntity> model, CancellationToken cancellationToken = default(CancellationToken));
    }
}