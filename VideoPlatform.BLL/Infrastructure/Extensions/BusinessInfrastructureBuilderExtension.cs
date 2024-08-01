using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.DAL;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Infrastructure.Extensions;

public static class BusinessInfrastructureBuilderExtension
{
    public static void AddBusinessInfrastructureBuilder(this IApplicationBuilder app,
        UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<VideoPlatformContext>();
        context?.Database.Migrate();

        IdentityDataInitializer.SeedData(userManager, roleManager);
    }

    public static async Task<bool> GetSchemeSupportsSignOutAsync(this HttpContext context, string scheme)
    {
        var provider = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
        var handler = await provider.GetHandlerAsync(context, scheme);
        return handler != null && handler is IAuthenticationSignOutHandler;
    }
}