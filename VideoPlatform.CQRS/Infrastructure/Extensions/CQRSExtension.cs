using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.CQRS.Infrastructure.Extensions;

public static class CQRSExtension
{
    extension(IServiceCollection services)
    {
        public void AddCQRS(Assembly assembly)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }
    }
}