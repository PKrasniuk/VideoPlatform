using System;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.NotificationService.Hubs;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        internal static IServiceCollection AddSignalRConfiguration(this IServiceCollection services)
        {
            services.AddSignalR(hubOptions =>
                {
                    hubOptions.EnableDetailedErrors = true;
                    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
                })
                .AddHubOptions<NotificationHub>(options => { });

            return services;
        }
    }
}