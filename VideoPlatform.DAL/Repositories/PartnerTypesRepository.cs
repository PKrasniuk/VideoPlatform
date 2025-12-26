using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class PartnerTypesRepository(VideoPlatformContext dbContext)
    : EntityRepository<PartnerTypes, int>(dbContext), IPartnerTypesRepository;