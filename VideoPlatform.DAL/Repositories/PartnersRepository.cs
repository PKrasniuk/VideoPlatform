using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PartnersRepository(VideoPlatformContext dbContext)
    : EntityRepository<Partner, int>(dbContext), IPartnersRepository;