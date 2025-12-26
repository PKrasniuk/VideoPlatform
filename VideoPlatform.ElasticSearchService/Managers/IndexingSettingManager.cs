using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingSettingManager(IElasticClient elasticClient)
    : IndexingEntityManager<Setting>(elasticClient), IIndexingSettingManager;