using Microsoft.AspNetCore.Builder;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions
{
    internal static partial class BuilderExtension
    {
        internal static void AddCorsBuilder(this IApplicationBuilder app)
        {
            app.UseCors(options =>
                options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        }
    }
}