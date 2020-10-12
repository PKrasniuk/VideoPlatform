using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.CacheService.Interfaces;
using VideoPlatform.CacheService.Repositories;

namespace VideoPlatform.CacheService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            services.AddTransient<ICacheRepository, CacheRepository>();

            return services;
        }
    }
}