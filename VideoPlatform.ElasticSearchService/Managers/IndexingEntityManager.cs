using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using VideoPlatform.ElasticSearchService.Interfaces;
using VideoPlatform.ElasticSearchService.Models;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public abstract class IndexingEntityManager<TEntity> : IIndexingEntityManager<TEntity> where TEntity : class
    {
        private readonly IElasticClient _elasticClient;

        protected IndexingEntityManager(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public async Task IndexEntityAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var status = await _elasticClient.DocumentExistsAsync<TEntity>(entity, null, cancellationToken);
            if (!status.Exists)
            {
                await _elasticClient.IndexDocumentAsync(entity, cancellationToken);
            }
        }

        public async Task UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _elasticClient.UpdateAsync<TEntity>(entity, u => u.Doc(entity), cancellationToken);
        }

        public async Task RemoveEntityAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _elasticClient.DeleteAsync<TEntity>(entity, null, cancellationToken);
        }

        public async Task IsEntityExist(TEntity entity, CancellationToken cancellationToken)
        {
            await _elasticClient.DocumentExistsAsync<TEntity>(entity, null, cancellationToken);
        }

        public async Task ReIndex(ICollection<TEntity> entities, CancellationToken cancellationToken)
        {
            if (entities != null && entities.Any())
            {
                await _elasticClient.DeleteByQueryAsync<TEntity>(q => q.MatchAll(), cancellationToken);

                foreach (var entity in entities)
                {
                    await _elasticClient.IndexDocumentAsync(entity, cancellationToken);
                }
            }
        }

        public async Task<FilterResult<TEntity>> Find(Filter<TEntity> model, CancellationToken cancellationToken)
        {
            var fullResult = await _elasticClient.SearchAsync<TEntity>(s =>
                s.Query(q => q.MultiMatch(x => x.Query(model.FilterQuery))), cancellationToken); // TODO

            var sortedProperty = model.SortedProperty.Body.Type.Name.ToLower().Equals("string")
                ? model.SortedProperty.AppendSuffix("keyword")
                : model.SortedProperty;

            var result = await _elasticClient.SearchAsync<TEntity>(s =>
                s.From((model.PageNumber - 1) * model.PageSize).Size(model.PageSize) // +
                    .Query(q => q.MultiMatch(x => x.Query(model.FilterQuery))) // TODO
                    .Sort(x => model.SortOrder == SortOrder.Ascending // +
                        ? x.Ascending(sortedProperty)
                        : x.Descending(sortedProperty)), cancellationToken);

            return fullResult?.Documents != null && result?.Documents != null
                ? new FilterResult<TEntity>
                {
                    Items = result.Documents.ToList(),
                    TotalCount = fullResult.Documents.Count
                }
                : new FilterResult<TEntity>();
        }
    }
}