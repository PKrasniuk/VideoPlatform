using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces;

public interface IPlaylistsRepository : IEntityRepository<Playlist, int>
{
}