using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoPlatform.DAL;
using VideoPlatform.Domain.Entities;

namespace VideoPlatform.BLL.Infrastructure.Extensions
{
    public static class BusinessInfrastructureBuilderExtension
    {
        public static IApplicationBuilder AddBusinessInfrastructureBuilder(this IApplicationBuilder app,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<VideoPlatformContext>();
            context?.Database.Migrate();

            IdentityDataInitializer.SeedData(userManager, roleManager);

            return app;
        }
    }
}