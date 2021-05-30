using Microsoft.AspNetCore.Builder;
using Ocelot.Middleware;

namespace VideoPlatform.ApiGateway.Infrastructure.Extensions
{
    internal static partial class BuilderExtension
    {
        internal static IApplicationBuilder AddSwaggerBuilder(this IApplicationBuilder app)
        {
            app.UseSwaggerForOcelotUI(options => { options.DownstreamSwaggerEndPointBasePath = "/swagger/docs"; })
                .UseOcelot().Wait();

            return app;
        }
    }
}