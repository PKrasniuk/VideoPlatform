using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.Api.Infrastructure.Filters;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Filters;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        internal static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Info {Title = "Video Platform API", Version = "v1"});
                    c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                    c.EnableAnnotations();
                    c.SchemaFilter<NullableTypeSchemaFilter>();
                    c.SchemaFilter<DefaultValueSchemaFilter>();
                    c.OperationFilter<FormFileSwaggerFilter>();
                    c.DescribeAllEnumsAsStrings();

                    c.AddSecurityDefinition(ConfigurationConstants.SecurityDefinitionName, new OAuth2Scheme
                    {
                        Type = configuration["Security:SwaggerSecurityDefinition:Type"],
                        Flow = configuration["Security:SwaggerSecurityDefinition:Flow"],
                        AuthorizationUrl = configuration["Security:SwaggerSecurityDefinition:AuthorizationUrl"],
                        Scopes = new Dictionary<string, string>
                        {
                            {"readAccess", "Access read operations"},
                            {"writeAccess", "Access write operations"}
                        }
                    });

                    c.OperationFilter<SecurityRequirementsOperationFilter>();
                });

            return services;
        }
    }
}