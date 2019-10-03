using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.BL.MediatR.Auth;
using DotNetAcademyPortal.BL.Profiles;
using DotNetAcademyPortal.BL.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetAcademyPortal.ServiceLayer.StartupConfig
{
    public static class CustomServicesConfig
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(CustomerProfile).Assembly);
            services.AddMediatR(typeof(LoginRequestHandler).Assembly);
            services.AddLogging();

            return services;
        }
    }
}
