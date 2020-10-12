using System;
using Microsoft.AspNetCore.Identity;
using VideoPlatform.DAL.Infrastructure.Helpers;
using VideoPlatform.Domain.Entities;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.DAL
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByNameAsync("superAdmin").Result == null)
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

                var result = userManager.CreateAsync(user, "super@Admin023").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserType.Admin.ToString()).Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<AppRole> roleManager)
        {
            foreach (var userType in (UserType[])Enum.GetValues(typeof(UserType)))
            {
                if (!roleManager.RoleExistsAsync(userType.ToString()).Result)
                {
                    var identityResult = roleManager.CreateAsync(new AppRole
                    {
                        Name = userType.ToString(),
                        NormalizedName = userType.ToString().ToUpperInvariant(),
                        Description = userType.GetDescription()
                    }).Result;
                }
            }
        }
    }
}