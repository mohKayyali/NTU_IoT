using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NTU.IoT.Models;

namespace NTU.IoT.Utility
{
    public static class DefaultAdminSetup
    {
        public static void CreateDefaultAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Check if the admin role exists; create it if not
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                var role = new IdentityRole("Admin");
                roleManager.CreateAsync(role).Wait();
                role = new IdentityRole("User");
                roleManager.CreateAsync(role).Wait();
                role = new IdentityRole("Super User");
                roleManager.CreateAsync(role).Wait();
            }

            // Check if the admin user exists; create it if not
            var adminUser = userManager.FindByNameAsync("admin").Result;
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    IsActive=true
                };

                userManager.CreateAsync(adminUser, "adminNTU123!").Wait();
                userManager.AddToRoleAsync(adminUser, "admin").Wait();
            }
        }
    }

}

