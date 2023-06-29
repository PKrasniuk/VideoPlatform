using System;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VideoPlatform.Common.Infrastructure.Filters;

/// <summary>
///     NullableTypeSchemaFilter
/// </summary>
public class NullableTypeSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (model.Properties == null) return;

        foreach (var (key, value) in model.Properties)
        {
            var property = context.Type.GetProperty(key,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property != null && property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                value.Default = null;
                value.Extensions.Add("nullable", new OpenApiBoolean(true));
                value.Example = null;
            }
        }
    }
}