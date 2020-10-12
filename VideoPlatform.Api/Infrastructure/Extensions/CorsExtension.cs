using System;
using System.Linq;
using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        internal static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfigurationRoot appConfiguration)
        {
            services.AddCors(
                options => options.AddPolicy(
                    ConfigurationConstants.DefaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
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