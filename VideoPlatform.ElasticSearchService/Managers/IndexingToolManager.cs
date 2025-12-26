using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingToolManager(IElasticClient elasticClient)
    : IndexingEntityManager<Tool>(elasticClient), IIndexingToolManager;