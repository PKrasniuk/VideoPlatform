using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Administration;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

namespace VideoPlatform.ApiGateway.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        internal static IServiceCollection AddOcelotConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerForOcelot(configuration);
            services.AddOcelot(configuration)
                .AddCacheManager(x => { x.WithDictionaryHandle(); })
                .AddAdministration("/administration", "secret");

            return services;
        }
    }
}
