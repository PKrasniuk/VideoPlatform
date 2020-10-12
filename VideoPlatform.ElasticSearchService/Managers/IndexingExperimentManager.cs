using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingExperimentManager : IndexingEntityManager<Experiment>, IIndexingExperimentManager
    {
        public IndexingExperimentManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}