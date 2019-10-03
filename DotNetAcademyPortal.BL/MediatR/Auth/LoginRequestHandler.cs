using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNetAcademyPortal.BL.Services;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.Interfaces;
using DotNetAcademyPortal.Common.MediatR.Auth.Requests;
using DotNetAcademyPortal.Common.MediatR.Auth.Responses;
using DotNetAcademyPortal.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DotNetAcademyPortal.BL.MediatR.Auth
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginRequestHandler(ITokenService tokenService, UserManager<ApplicationUser> userManager, DotNetAcademyPortalDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        async Task<LoginResponse> IRequestHandler<LoginRequest, LoginResponse>.Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var theUser = await _userManager.FindByNameAsync(request.Login.UserName);
            if (theUser != null && await _userManager.CheckPasswordAsync(theUser, request.Login.Password))
            {
                var token = await _tokenService.GenerateToken(theUser);
                await _signInManager.SignInAsync(theUser, true);
                var isAdmin = await _userManager.IsInRoleAsync(theUser, "Administrator");
                var name = isAdmin ? theUser.UserName : _context.Customers.First(c => c.CustomerId == theUser.Id).Name;
                return new LoginResponse()
                {
                    Token = token,
                    UserName = name,
                    Id = theUser.Id,
                    IsAdmin = isAdmin
                };
            }

            return new LoginResponse()
            {
                Error = "Onbekende combinatie"
            };
        }
    }
}
