using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingSeriesManager : IndexingEntityManager<Series>, IIndexingSeriesManager
    {
        public IndexingSeriesManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}