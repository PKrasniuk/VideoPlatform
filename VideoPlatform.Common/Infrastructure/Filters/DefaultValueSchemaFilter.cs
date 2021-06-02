using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VideoPlatform.Common.Infrastructure.Filters
{
    /// <summary>
    /// DefaultValueSchemaFilter
    /// </summary>
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            foreach (var propertyInfo in context.Type.GetProperties())
            {
                var defaultAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultAttribute != null)
                {
                    foreach (var (key, value) in schema.Properties)
                    {
                        if (ToCamelCase(propertyInfo.Name) == key)
                        {
                            value.Example = (IOpenApiAny) defaultAttribute.Value;
                            break;
                        }
                    }
                }
            }
        }

        private static string ToCamelCase(string name)
        {
            return char.ToLowerInvariant(name[0]) + name[1..];
        }
    }
}