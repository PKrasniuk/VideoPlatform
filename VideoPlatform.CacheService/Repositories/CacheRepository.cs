using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using VideoPlatform.CacheService.Interfaces;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.CacheService.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _cache;

        public CacheRepository(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task RefreshAsync(string key, CancellationToken cancellationToken)
        {
            await _cache.RefreshAsync(key, cancellationToken);
        }

        public async Task<bool> ExistObjectAsync(string key, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _cache.GetStringAsync(key, cancellationToken);
                return value != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SetObjectAsync<T>(string key, T value, int expirationMinutes = ConfigurationConstants.DefaultExpirationMinutes, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value),
                new DistributedCacheEntryOptions {AbsoluteExpiration = DateTime.Now.AddMinutes(expirationMinutes)},
                cancellationToken);
        }

        public async Task<T> GetObjectAsync<T>(string key, CancellationToken cancellationToken)
        {
            var value = await _cache.GetStringAsync(key, cancellationToken);
            return string.IsNullOrEmpty(value) ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }
    }
}