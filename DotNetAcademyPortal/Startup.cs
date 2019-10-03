using System;
using System.Data.Common;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.BL.MediatR.Auth;
using DotNetAcademyPortal.BL.Profiles;
using DotNetAcademyPortal.BL.Services;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using DotNetAcademyPortal.ServiceLayer.AppConfig;
using DotNetAcademyPortal.ServiceLayer.StartupConfig;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DotNetAcademyPortal.ServiceLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddJwtAuthentication(appSettingsSection.Get<AppSettings>());

            services.AddCustomServices();

            services.AddAuthorization();

            services.AddDbConfig(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UpdateDatabase();

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseSerilogRequestLogging();

            app.UseMvc();

            app.UseSpa(env);

            app.CreateRoles(serviceProvider, Configuration).Wait();
        }
    }
}
