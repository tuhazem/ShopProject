using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using ShopProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Infrastructure
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@shop.com",
                FullName = "System Admin",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != adminUser.Id))
            { 
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null) { 
                
                   await userManager.CreateAsync(adminUser, "P@ssword123");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

        }
    }
}
