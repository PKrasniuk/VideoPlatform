using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingPartnerTypesManager : IndexingEntityManager<PartnerTypes>, IIndexingPartnerTypesManager
    {
        public IndexingPartnerTypesManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}