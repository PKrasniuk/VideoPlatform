using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingSubscriptionSeriesManager(IElasticClient elasticClient)
    : IndexingEntityManager<SubscriptionSeries>(elasticClient),
        IIndexingSubscriptionSeriesManager;