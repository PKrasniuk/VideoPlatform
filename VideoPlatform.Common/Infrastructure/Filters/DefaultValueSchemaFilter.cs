using System.ComponentModel;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VideoPlatform.Common.Infrastructure.Filters
{
    /// <summary>
    /// DefaultValueSchemaFilter
    /// </summary>
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            foreach (var propertyInfo in context.SystemType.GetProperties())
            {

                var defaultAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultAttribute != null)
                {
                    foreach (var (key, value) in schema.Properties)
                    {
                        if (ToCamelCase(propertyInfo.Name) == key)
                        {
                            value.Example = defaultAttribute.Value;
                            break;
                        }
                    }
                }
            }
        }

        private static string ToCamelCase(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}