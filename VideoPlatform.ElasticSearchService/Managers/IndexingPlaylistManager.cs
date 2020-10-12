using Nest;
using VideoPlatform.Domain.Entities;
using VideoPlatform.ElasticSearchService.Interfaces;

namespace VideoPlatform.ElasticSearchService.Managers
{
    public class IndexingPlaylistManager : IndexingEntityManager<Playlist>, IIndexingPlaylistManager
    {
        public IndexingPlaylistManager(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}