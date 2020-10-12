using VideoPlatform.Domain.Entities;

namespace VideoPlatform.DAL.Interfaces
{
    public interface ISettingsRepository : IEntityRepository<Setting, short>
    {
    }
}