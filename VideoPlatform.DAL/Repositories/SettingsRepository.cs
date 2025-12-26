using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories;

public class SettingsRepository(VideoPlatformContext dbContext)
    : EntityRepository<Setting, short>(dbContext), ISettingsRepository;