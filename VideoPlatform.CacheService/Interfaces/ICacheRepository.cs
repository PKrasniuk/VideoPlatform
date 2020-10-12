using System.Threading;
using System.Threading.Tasks;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.CacheService.Interfaces
{
    public interface ICacheRepository
    {
        Task RefreshAsync(string key, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> ExistObjectAsync(string key, CancellationToken cancellationToken = default(CancellationToken));

        Task SetObjectAsync<T>(string key, T value, int expirationMinutes = ConfigurationConstants.DefaultExpirationMinutes, CancellationToken cancellationToken = default(CancellationToken));

        Task<T> GetObjectAsync<T>(string key, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveAsync(string key, CancellationToken cancellationToken = default(CancellationToken));
    }
}