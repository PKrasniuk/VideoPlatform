using System;
using System.Reflection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.MicrosoftExtensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VideoPlatform.Common.Infrastructure.Filters;

/// <summary>
///     NullableTypeSchemaFilter
/// </summary>
public class NullableTypeSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema model, SchemaFilterContext context)
    {
        if (model.Properties == null) return;

        foreach (var (key, value) in model.Properties)
        {
            var property = context.Type.GetProperty(key,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property != null && property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                value.Extensions?.Add("nullable", new OpenApiDeprecationExtension());
        }
    }
}