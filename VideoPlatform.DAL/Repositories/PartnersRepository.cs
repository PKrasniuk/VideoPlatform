using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PartnersRepository : EntityRepository<Partner, int>, IPartnersRepository
{
    public PartnersRepository(VideoPlatformContext dbContext) : base(dbContext)
    {
    }
}