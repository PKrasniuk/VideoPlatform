using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingExperimentManager(IElasticClient elasticClient)
    : IndexingEntityManager<Experiment>(elasticClient), IIndexingExperimentManager;