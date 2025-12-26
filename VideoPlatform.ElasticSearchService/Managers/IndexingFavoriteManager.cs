using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingFavoriteManager(IElasticClient elasticClient)
    : IndexingEntityManager<Favorite>(elasticClient), IIndexingFavoriteManager;