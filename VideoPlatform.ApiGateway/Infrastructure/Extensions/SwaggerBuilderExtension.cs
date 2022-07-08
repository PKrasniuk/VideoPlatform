using Microsoft.AspNetCore.Builder;
using Ocelot.Middleware;

namespace VideoPlatform.ApiGateway.Infrastructure.Extensions
{
    internal static class BuilderExtension
    {
        internal static void AddSwaggerBuilder(this IApplicationBuilder app)
        {
            app.UseSwaggerForOcelotUI(options => { options.DownstreamSwaggerEndPointBasePath = "/swagger/docs"; })
                .UseOcelot().Wait();
        }
    }
}