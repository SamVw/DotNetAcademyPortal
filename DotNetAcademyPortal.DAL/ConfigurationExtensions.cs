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
        public static async Task InitializeAndMigrateDatabase(this IApplicationBuilder app, IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            UpdateDatabase(app);

            await CreateRoles(app, serviceProvider, configuration);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
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

        private static async Task CreateRoles(IApplicationBuilder app, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<DotNetAcademyPortalDbContext>();
            string[] roleNames = { "Administrator", "Customer" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var powerUser = new ApplicationUser
            {
                UserName = configuration.GetSection("UserSettings")["UserName"]
            };

            string userPassword = configuration.GetSection("UserSettings")["UserPassword"];
            var user = await userManager.FindByEmailAsync(configuration.GetSection("UserSettings")["UserName"]);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(powerUser, "Administrator");
                    
                    Admin admin = new Admin() { ApplicationUser = powerUser };
                    context.Admins.Add(admin);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
