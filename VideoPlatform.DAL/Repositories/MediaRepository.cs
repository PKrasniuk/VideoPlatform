using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class MediaRepository : EntityRepository<Media, long>, IMediaRepository
{
    public MediaRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}