using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingSubscriptionTopicManager(IElasticClient elasticClient)
    : IndexingEntityManager<SubscriptionTopic>(elasticClient),
        IIndexingSubscriptionTopicManager;