using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.Api.Infrastructure.HealthCheck;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        public static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("ElasticSearch", new PingHealthCheck(configuration["ElasticSearch:url"], 100))
                .AddCheck("Redis", new PingHealthCheck(configuration["Cache:RedisUrl"], 100))
                .AddCheck("Authorization",
                    new PingHealthCheck(configuration["Security:SwaggerSecurityDefinition:AuthorizationUrl"], 100))
                .AddCheck<SystemMemoryHealthCheck>("Memory");
        }
    }
}