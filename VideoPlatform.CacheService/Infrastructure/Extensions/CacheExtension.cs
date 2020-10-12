using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.CacheService.Infrastructure.Extensions
{
    public static partial class ConfigurationExtension
    {
        public static IServiceCollection AddCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration["Cache:RedisUrl"];
                options.InstanceName = configuration["Cache:InstanceName"];
            });

            return services;
        }
    }
}