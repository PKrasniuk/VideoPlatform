using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingToolManager : IndexingEntityManager<Tool>, IIndexingToolManager
    {
        public IndexingToolManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}