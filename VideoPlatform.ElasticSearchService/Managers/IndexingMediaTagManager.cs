using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingMediaTagManager : IndexingEntityManager<MediaTag>, IIndexingMediaTagManager
{
    public IndexingMediaTagManager(IElasticClient elasticClient) : base(elasticClient)
    {
    }
}