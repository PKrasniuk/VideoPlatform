using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VideoPlatform.Api.Models;

namespace VideoPlatform.Api.Infrastructure.Extensions;

internal static class MappingProfileExtension
{
    internal static void AddMappingConfiguration(this IServiceCollection services)
    {
        var mappingConfig =
            new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }, new LoggerFactory());

        services.AddSingleton(mappingConfig.CreateMapper());
    }
}