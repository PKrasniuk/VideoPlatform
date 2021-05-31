using Microsoft.AspNetCore.Builder;
using VideoPlatform.NotificationService.Hubs;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions
{
    internal static partial class BuilderExtension
    {
        internal static IApplicationBuilder AddSignalRBuilder(this IApplicationBuilder app)
        {
            app.UseEndpoints(routes =>
            {
                routes.MapHub<NotificationHub>("/api/notification");
            });

            return app;
        }
    }
}