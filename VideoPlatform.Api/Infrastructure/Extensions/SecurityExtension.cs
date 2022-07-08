using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class ConfigurationExtension
    {
        public static void AddSecurityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["Security:Authority"];
                    options.ApiName = configuration["Security:ApiName"];
                    options.RequireHttpsMetadata = bool.Parse(configuration["Security:RequireHttpsMetadata"]); //dev only
                });

            services.AddAuthorization(c =>
            {
                c.AddPolicy("readAccess", p => p.RequireClaim("scope", "readAccess"));
                c.AddPolicy("writeAccess", p => p.RequireClaim("scope", "writeAccess"));
            });
        }
    }
}