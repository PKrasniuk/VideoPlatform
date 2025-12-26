using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingMediaTagManager(IElasticClient elasticClient)
    : IndexingEntityManager<MediaTag>(elasticClient), IIndexingMediaTagManager;