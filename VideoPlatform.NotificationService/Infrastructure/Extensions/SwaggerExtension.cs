using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using VideoPlatform.Common.Infrastructure.Filters;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        internal static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Info {Title = "Video Platform Notification API", Version = "v1"});
                    c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                    c.EnableAnnotations();
                    c.SchemaFilter<NullableTypeSchemaFilter>();
                    c.SchemaFilter<DefaultValueSchemaFilter>();
                    c.OperationFilter<FormFileSwaggerFilter>();
                    c.DescribeAllEnumsAsStrings();
                });

            return services;
        }
    }
}