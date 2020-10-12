using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingSubscriptionSeriesManager : IndexingEntityManager<SubscriptionSeries>, IIndexingSubscriptionSeriesManager
    {
        public IndexingSubscriptionSeriesManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}