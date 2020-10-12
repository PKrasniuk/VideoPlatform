using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using VideoPlatform.Common.Infrastructure.Configurations;

namespace VideoPlatform.Common.Infrastructure.Extensions
{
    public static class HostingEnvironmentExtension
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {
            return AppConfiguration.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}