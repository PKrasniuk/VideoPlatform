using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using VideoPlatform.Api.Infrastructure.Filters;
using VideoPlatform.Common.Infrastructure.Constants;
using VideoPlatform.Common.Infrastructure.Filters;

namespace VideoPlatform.Api.Infrastructure.Extensions;

internal static partial class ConfigurationExtension
{
    internal static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Video Platform API", Version = "v1" });
            c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            c.EnableAnnotations();
            c.SchemaFilter<NullableTypeSchemaFilter>();

            c.AddSecurityDefinition(ConfigurationConstants.SecurityDefinitionName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl =
                            new Uri(configuration["Security:SwaggerSecurityDefinition:AuthorizationUrl"]),
                        TokenUrl = new Uri(configuration["Security:SwaggerSecurityDefinition:TokenUrl"]),
                        Scopes = new Dictionary<string, string>
                        {
                            { "readAccess", "Access read operations" },
                            { "writeAccess", "Access write operations" }
                        }
                    }
                }
            });

            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        services.AddSwaggerGenNewtonsoftSupport();
    }
}