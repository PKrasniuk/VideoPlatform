using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.CacheService.Infrastructure.Extensions;

public static partial class ConfigurationExtension
{
    public static void AddCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedMemoryCache();
    }
}