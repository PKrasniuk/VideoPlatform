using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class PlaylistMediaRepository : EntityRepository<PlaylistMedia, int>, IPlaylistMediaRepository
    {
        public PlaylistMediaRepository(VideoPlatformContext dbContext) : base(dbContext)
        {
        }
    }
}