using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingTagManager : IndexingEntityManager<Tag>, IIndexingTagManager
    {
        public IndexingTagManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}