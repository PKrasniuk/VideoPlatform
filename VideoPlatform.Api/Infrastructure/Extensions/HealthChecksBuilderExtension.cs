using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using VideoPlatform.Api.Models.Settings;

namespace VideoPlatform.Api.Infrastructure.Extensions;

internal static partial class BuilderExtension
{
    internal static void AddHealthChecksBuilder(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/hc", new HealthCheckOptions
        {
            ResponseWriter = async (c, r) =>
            {
                c.Response.ContentType = "application/json";
                var result = new List<ServiceStatus>
                {
                    new() { Service = "OverAll", Status = r.Status }
                };
                result.AddRange(
                    r.Entries.Select(
                        e => new ServiceStatus
                        {
                            Service = e.Key,
                            Status = e.Value.Status,
                            Data = e.Value.Data.Select(k => k).ToList()
                        }
                    )
                );

                await c.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        });
    }
}