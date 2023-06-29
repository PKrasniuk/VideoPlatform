using VideoPlatform.Domain.Entities;

namespace VideoPlatform.ElasticSearchService.Interfaces;

public interface IIndexingPlaylistManager : IIndexingEntityManager<Playlist>
{
}