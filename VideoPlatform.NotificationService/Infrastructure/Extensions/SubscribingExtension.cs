using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.NotificationService.Interfaces;
using VideoPlatform.NotificationService.Managers;

namespace VideoPlatform.NotificationService.Infrastructure.Extensions;

internal static partial class ConfigurationExtension
{
    internal static void AddSubscribes(this IServiceCollection services)
    {
        services.AddTransient<ISubscriberManager, SubscriberManager>();
    }
}