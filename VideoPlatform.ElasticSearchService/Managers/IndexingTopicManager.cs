using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingTopicManager : IndexingEntityManager<Topic>, IIndexingTopicManager
    {
        public IndexingTopicManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}