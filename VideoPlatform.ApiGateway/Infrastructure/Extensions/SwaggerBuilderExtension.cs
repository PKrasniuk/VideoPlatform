using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Ocelot.Middleware;

namespace VideoPlatform.ApiGateway.Infrastructure.Extensions
{
    internal static partial class BuilderExtension
    {
        internal static IApplicationBuilder AddSwaggerBuilder(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwaggerForOcelotUI(configuration,
                    options => { options.DownstreamSwaggerEndPointBasePath = "/swagger/docs"; })
                .UseOcelot()
                .Wait();

            return app;
        }
    }
}