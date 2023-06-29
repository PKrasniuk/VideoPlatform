using Microsoft.AspNetCore.Builder;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions;

internal static partial class BuilderExtension
{
    internal static void AddSwaggerBuilder(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Platform Notification V1");
        });
    }
}