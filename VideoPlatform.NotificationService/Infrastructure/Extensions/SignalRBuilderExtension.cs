using Microsoft.AspNetCore.Builder;
using VideoPlatform.NotificationService.Hubs;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions;

internal static partial class BuilderExtension
{
    internal static void AddSignalRBuilder(this IApplicationBuilder app)
    {
        app.UseEndpoints(routes => { routes.MapHub<NotificationHub>("/api/notification"); });
    }
}