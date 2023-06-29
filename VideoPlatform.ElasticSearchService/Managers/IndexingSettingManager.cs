using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers;

public class IndexingSettingManager : IndexingEntityManager<Setting>, IIndexingSettingManager
{
    public IndexingSettingManager(IElasticClient elasticClient) : base(elasticClient)
    {
    }
}