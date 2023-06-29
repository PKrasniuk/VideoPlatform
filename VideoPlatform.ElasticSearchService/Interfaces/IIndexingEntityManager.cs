using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.ElasticSearchService.Models;

namespace VideoPlatform.ElasticSearchService.Interfaces;

public interface IIndexingEntityManager<TEntity>
{
    Task IndexEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveEntityAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task IsEntityExist(TEntity entity, CancellationToken cancellationToken = default);

    Task ReIndex(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

    Task<FilterResult<TEntity>> Find(Filter<TEntity> model, CancellationToken cancellationToken = default);
}