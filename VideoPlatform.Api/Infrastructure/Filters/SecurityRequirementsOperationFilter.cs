using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VideoPlatform.Common.Infrastructure.Constants;

namespace VideoPlatform.Api.Infrastructure.Filters
{
    /// <summary>
    /// SecurityRequirementsOperationFilter
    /// </summary>
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerScopes = context.MethodInfo?.DeclaringType?.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Select(attr => attr.Policy).ToList();
            if (controllerScopes != null)
            {
                var actionScopes = context.MethodInfo?.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                    .Select(attr => attr.Policy).ToList();
                if (actionScopes != null)
                {
                    var requiredScopes = controllerScopes.Union(actionScopes).Distinct().ToList();
                    if (requiredScopes.Any())
                    {
                        operation.Responses.Add(((int) HttpStatusCode.Unauthorized).ToString(),
                            new OpenApiResponse {Description = HttpStatusCode.Unauthorized.ToString()});
                        operation.Responses.Add(((int) HttpStatusCode.Forbidden).ToString(),
                            new OpenApiResponse {Description = HttpStatusCode.Forbidden.ToString()});
                        operation.Security = new List<OpenApiSecurityRequirement>
                        {
                            new()
                            {
                                {
                                    new OpenApiSecurityScheme {Name = ConfigurationConstants.SecurityDefinitionName},
                                    requiredScopes
                                }
                            }
                        };
                    }
                }
            }
        }
    }
}