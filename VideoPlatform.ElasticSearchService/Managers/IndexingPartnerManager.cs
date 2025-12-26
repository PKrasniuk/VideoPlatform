using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingPartnerManager(IElasticClient elasticClient)
    : IndexingEntityManager<Partner>(elasticClient), IIndexingPartnerManager;