using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PartnerTypesRepository : EntityRepository<PartnerTypes, int>, IPartnerTypesRepository
{
    public PartnerTypesRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}