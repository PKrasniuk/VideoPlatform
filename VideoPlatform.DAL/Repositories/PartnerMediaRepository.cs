using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PartnerMediaRepository : EntityRepository<PartnerMedia, int>, IPartnerMediaRepository
{
    public PartnerMediaRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}