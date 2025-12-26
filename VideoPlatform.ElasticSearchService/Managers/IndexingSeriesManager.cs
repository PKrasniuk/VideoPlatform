using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingSeriesManager(IElasticClient elasticClient)
    : IndexingEntityManager<Series>(elasticClient), IIndexingSeriesManager;