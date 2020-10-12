using System;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VideoPlatform.Common.Infrastructure.Filters
{
    /// <summary>
    /// NullableTypeSchemaFilter
    /// </summary>
    public class NullableTypeSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (model.Properties == null)
            {
                return;
            }

            foreach (var (key, value) in model.Properties)
            {
                var property = context.SystemType.GetProperty(key,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null && property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    value.Default = null;
                    value.Extensions.Add("nullable", true);
                    value.Example = null;
                }
            }
        }
    }
}