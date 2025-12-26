using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingPartnerTypesManager(IElasticClient elasticClient)
    : IndexingEntityManager<PartnerTypes>(elasticClient), IIndexingPartnerTypesManager;