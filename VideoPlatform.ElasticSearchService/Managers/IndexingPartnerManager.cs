using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingPartnerManager : IndexingEntityManager<Partner>, IIndexingPartnerManager
    {
        public IndexingPartnerManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}