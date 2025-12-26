using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PartnerMediaRepository(VideoPlatformContext dbContext)
    : EntityRepository<PartnerMedia, int>(dbContext), IPartnerMediaRepository;