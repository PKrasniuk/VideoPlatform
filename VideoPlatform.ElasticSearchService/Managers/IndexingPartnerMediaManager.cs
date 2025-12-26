using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingPartnerMediaManager(IElasticClient elasticClient)
    : IndexingEntityManager<PartnerMedia>(elasticClient), IIndexingPartnerMediaManager;