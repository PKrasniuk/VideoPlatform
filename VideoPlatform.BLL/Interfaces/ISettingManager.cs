using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Interfaces;

public interface ISettingManager
{
    Task<ICollection<Setting>> GetSettingsAsync(CancellationToken cancellationToken = default);

    Task<ICollection<Setting>> GetSettingsCQRSAsync(CancellationToken cancellationToken = default);

    Task<Setting> GetSettingCQRSAsync(short id);

    Task<Setting> AddSettingCQRSAsync(Setting entity);

    Task UpdateSettingCQRSAsync(Setting entity);

    Task RemoveSettingCQRSAsync(short id);
}