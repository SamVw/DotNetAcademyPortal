using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetAcademyPortal.ServiceLayer.StartupConfig
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DotNetAcademyPortalDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DotNetAcademyPortalDbContext>();

            return services;
        }
    }
}
