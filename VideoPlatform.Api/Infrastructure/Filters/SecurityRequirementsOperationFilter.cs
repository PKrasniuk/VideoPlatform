using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
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
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var controllerScopes = context.MethodInfo?.DeclaringType?.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Select(attr => attr.Policy).ToList();
            if (controllerScopes != null)
            {
                var actionScopes = context.MethodInfo?.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                    .Select(attr => attr.Policy).ToList();
                var requiredScopes = controllerScopes.Union(actionScopes).Distinct().ToList();
                if (requiredScopes.Any())
                {
                    operation.Responses.Add(((int) HttpStatusCode.Unauthorized).ToString(),
                        new Response {Description = HttpStatusCode.Unauthorized.ToString()});
                    operation.Responses.Add(((int) HttpStatusCode.Forbidden).ToString(),
                        new Response {Description = HttpStatusCode.Forbidden.ToString()});

                    operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                    {
                        new Dictionary<string, IEnumerable<string>>
                            {{ConfigurationConstants.SecurityDefinitionName, requiredScopes}}
                    };
                }
            }
        }
    }
}