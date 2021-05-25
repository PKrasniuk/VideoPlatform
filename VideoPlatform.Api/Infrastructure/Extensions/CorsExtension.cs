using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        internal static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(
                options => options.AddPolicy(
                    ConfigurationConstants.DefaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                //.Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                )
            );

            return services;
        }
    }
}