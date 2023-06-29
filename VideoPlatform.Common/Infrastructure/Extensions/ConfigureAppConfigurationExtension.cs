using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using VideoPlatform.Common.Models.ConfigurationModels;

namespace VideoPlatform.Common.Infrastructure.Extensions;

public static class ConfigureAppConfigurationExtension
{
    public static IHostBuilder InitAppConfiguration(this IHostBuilder host, TransientFaultHandlingOptions options)
    {
        return host.ConfigureAppConfiguration((hostingContext, configuration) =>
        {
            configuration.Sources.Clear();

            var env = hostingContext.HostingEnvironment;

            configuration
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

            configuration.Build().GetSection(nameof(TransientFaultHandlingOptions)).Bind(options);
        });
    }
}