using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingSubscriptionTopicManager : IndexingEntityManager<SubscriptionTopic>,
    IIndexingSubscriptionTopicManager
{
    public IndexingSubscriptionTopicManager(IElasticClient elasticClient) : base(elasticClient)
    {
    }
}