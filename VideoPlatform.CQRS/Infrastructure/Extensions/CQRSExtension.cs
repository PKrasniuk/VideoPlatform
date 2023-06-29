using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.CQRS.Infrastructure.Extensions;

public static class CQRSExtension
{
    public static void AddCQRS(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(assembly);
    }
}