using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.CacheService.Infrastructure.Settings;

namespace VideoPlatform.CacheService.Infrastructure.Extensions
{
    public static partial class ConfigurationExtension
    {
        public static void AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheSettings>(configuration.GetSection("Cache"));
        }
    }
}