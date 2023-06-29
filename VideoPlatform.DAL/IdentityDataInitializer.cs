using System;
using Microsoft.AspNetCore.Identity;
using VideoPlatform.Common.Infrastructure.Helpers;
using VideoPlatform.DAL.Infrastructure.Helpers;
using VideoPlatform.Domain.Entities;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.DAL;

public static class IdentityDataInitializer
{
    public static void SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        SeedRoles(roleManager);
        SeedUsers(userManager);
    }

    private static void SeedUsers(UserManager<AppUser> userManager)
    {
        if (AsyncHelper.RunSync(async () => await userManager.FindByNameAsync("superAdmin")) == null)
        {
            var user = new AppUser
            {
                FirstName = "Super",
                LastName = "Admin",
                UserName = "superAdmin",
                NormalizedUserName = "superAdmin".ToUpperInvariant(),
                Email = "pkrasnyuk@hotmail.com",
                NormalizedEmail = "pkrasnyuk@hotmail.com".ToUpperInvariant(),
                EmailConfirmed = true,
                Status = UserStatus.Active,
                PhoneNumber = "+11111111111",
                PhoneNumberConfirmed = true
            };

            var result = AsyncHelper.RunSync(async () => await userManager.CreateAsync(user, "super@Admin023"));
            if (result.Succeeded)
                AsyncHelper.RunSync(async () => await userManager.AddToRoleAsync(user, UserType.Admin.ToString()));
        }
    }

    private static void SeedRoles(RoleManager<AppRole> roleManager)
    {
        foreach (var userType in (UserType[])Enum.GetValues(typeof(UserType)))
            if (!AsyncHelper.RunSync(async () => await roleManager.RoleExistsAsync(userType.ToString())))
                AsyncHelper.RunSync(async () => await roleManager.CreateAsync(new AppRole
                {
                    Name = userType.ToString(),
                    NormalizedName = userType.ToString().ToUpperInvariant(),
                    Description = userType.GetDescription()
                }));
    }
}