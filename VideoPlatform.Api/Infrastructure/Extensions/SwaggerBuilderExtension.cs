using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace VideoPlatform.Api.Infrastructure.Extensions
{
    internal static partial class BuilderExtension
    {
        internal static void AddSwaggerBuilder(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Platform V1");

                options.OAuthClientId(configuration["Security:ApiClient:ClientId"]);
                options.OAuthAppName(configuration["Security:ApiClient:AppName"]);
                options.OAuthClientSecret(configuration["Security:ApiClient:ClientSecret"].Sha256());
                options.OAuthScopeSeparator(configuration["Security:ApiClient:ScopeSeparator"]);
                options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            });
        }
    }
}