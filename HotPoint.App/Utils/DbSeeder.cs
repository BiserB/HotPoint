using HotPoint.Data;
using HotPoint.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotPoint.App.Utils
{
    public static class DbSeeder
    {
        public async static void SeedRoles(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var roles = new IdentityRole[]
                {
                    new IdentityRole(RoleType.Customer),
                    new IdentityRole(RoleType.Manager),
                    new IdentityRole(RoleType.Administrator),
                };

                foreach (var role in roles)
                {
                    var roleDoesNotExist = !await roleManager.RoleExistsAsync(role.Name);

                    if (roleDoesNotExist)
                    {
                        var creation = await roleManager.CreateAsync(role);

                        if (!creation.Succeeded)
                        {
                            throw new InvalidOperationException($"Creation of {role} role failed.");
                        }
                    }
                }
            }
        }

        public static async void SeedUsers(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();

                var admin = await userManager.FindByNameAsync(RoleType.Administrator);

                if (admin == null)
                {
                    admin = new AppUser()
                    {
                        UserName = "admin",
                        Email = "admin@hotpoint.bg",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(admin, "1111");

                    await userManager.AddToRoleAsync(admin, RoleType.Administrator);
                }
            }
        }
    }
}
