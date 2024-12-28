using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.Api.Infrastructure.Extensions;

internal static partial class ConfigurationExtension
{
    public static void AddSecurityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Security:Authority"];
                options.TokenValidationParameters.ValidAudiences = new List<string>
                    { configuration[configuration["Security:ApiName"] ?? string.Empty] };
                options.RequireHttpsMetadata =
                    bool.Parse(configuration["Security:RequireHttpsMetadata"] ?? string.Empty); //dev only
            });

        services.AddAuthorization(c =>
        {
            c.AddPolicy("readAccess", p => p.RequireClaim("scope", "readAccess"));
            c.AddPolicy("writeAccess", p => p.RequireClaim("scope", "writeAccess"));
        });
    }
}