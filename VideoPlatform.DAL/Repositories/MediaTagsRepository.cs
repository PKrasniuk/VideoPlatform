using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class MediaTagsRepository : EntityRepository<MediaTag, int>, IMediaTagsRepository
    {
        public MediaTagsRepository(VideoPlatformContext dbContext) : base(dbContext)
        {
        }
    }
}