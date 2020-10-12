using System.Collections.Concurrent;
using System.IO;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;

namespace VideoPlatform.Common.Infrastructure.Configurations
{
    public static class AppConfiguration
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

        static AppConfiguration()
        {
            ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        internal static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return ConfigurationCache.GetOrAdd(cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets));
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configurationFile = Path.Combine("configuration", "configuration.json");
            if (File.Exists(configurationFile))
            {
                builder = builder.AddJsonFile(configurationFile);
            }

            if (!string.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                builder.AddUserSecrets(typeof(AppConfiguration).GetAssembly(), true);
            }

            return builder.Build();
        }
    }
}