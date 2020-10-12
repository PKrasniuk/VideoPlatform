using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.CQRS.Infrastructure.Extensions
{
    public static class CQRSExtension
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, System.Reflection.Assembly assembly)
        {
            services.AddMediatR(assembly);

            return services;
        }
    }
}