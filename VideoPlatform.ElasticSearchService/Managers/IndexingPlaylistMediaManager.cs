using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingPlaylistMediaManager : IndexingEntityManager<PlaylistMedia>, IIndexingPlaylistMediaManager
    {
        public IndexingPlaylistMediaManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}