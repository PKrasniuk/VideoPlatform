using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingFavoriteManager : IndexingEntityManager<Favorite>, IIndexingFavoriteManager
{
    public IndexingFavoriteManager(IElasticClient elasticClient) : base(elasticClient)
    {
    }
}