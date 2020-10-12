using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class PlaylistsRepository : EntityRepository<Playlist, int>, IPlaylistsRepository
    {
        public PlaylistsRepository(VideoPlatformContext dbContext) : base(dbContext)
        {
        }
    }
}