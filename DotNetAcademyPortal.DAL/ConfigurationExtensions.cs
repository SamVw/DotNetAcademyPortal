using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetAcademyPortal.DAL
{
    public static class ConfigurationExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DotNetAcademyPortalDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public static async Task CreateRoles(this IApplicationBuilder app, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<DotNetAcademyPortalDbContext>();
            string[] roleNames = { "Administrator", "Customer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var powerUser = new ApplicationUser
            {
                UserName = configuration.GetSection("UserSettings")["UserName"]
            };

            string userPassword = configuration.GetSection("UserSettings")["UserPassword"];
            var user = await UserManager.FindByEmailAsync(configuration.GetSection("UserSettings")["UserName"]);

            if (user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(powerUser, "Administrator");
                    
                    Admin admin = new Admin() { ApplicationUser = powerUser };
                    context.Admins.Add(admin);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
