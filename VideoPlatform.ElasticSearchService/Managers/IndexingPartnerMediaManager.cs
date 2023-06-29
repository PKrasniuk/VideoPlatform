using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingPartnerMediaManager : IndexingEntityManager<PartnerMedia>, IIndexingPartnerMediaManager
{
    public IndexingPartnerMediaManager(IElasticClient elasticClient) : base(elasticClient)
    {
    }
}