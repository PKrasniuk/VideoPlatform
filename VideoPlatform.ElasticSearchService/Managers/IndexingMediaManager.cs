using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingMediaManager : IndexingEntityManager<Media>, IIndexingMediaManager
    {
        public IndexingMediaManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}