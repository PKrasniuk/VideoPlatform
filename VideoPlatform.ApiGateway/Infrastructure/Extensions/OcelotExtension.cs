using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Administration;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

namespace VideoPlatform.ApiGateway.Infrastructure.Extensions;

internal static class ConfigurationExtension
{
    internal static void AddOcelotConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerForOcelot(configuration);
        services.AddOcelot(configuration)
            .AddCacheManager(x => { x.WithDictionaryHandle(); })
            .AddAdministration("/administration", "secret");
    }
}