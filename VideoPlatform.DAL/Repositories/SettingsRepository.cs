using VideoPlatform.DAL.Interfaces;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Repositories
{
    public class SettingsRepository : EntityRepository<Setting, short>, ISettingsRepository
    {
        public SettingsRepository(VideoPlatformContext dbContext) : base(dbContext)
        {
        }
    }
}