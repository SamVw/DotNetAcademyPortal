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
using DotNetAcademyPortal.Common.MediatR.Auth.Requests;
using DotNetAcademyPortal.Common.MediatR.Auth.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DotNetAcademyPortal.BL.MediatR.Auth
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginRequestHandler(ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        async Task<LoginResponse> IRequestHandler<LoginRequest, LoginResponse>.Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var theUser = await _userManager.FindByNameAsync(request.Login.UserName);
            if (theUser != null && await _userManager.CheckPasswordAsync(theUser, request.Login.Password))
            {
                var token = await _tokenService.GenerateToken(theUser);
                return new LoginResponse()
                {
                    Token = token,
                    UserName = theUser.UserName,
                    IsAdmin = await _userManager.IsInRoleAsync(theUser, "Administrator")
                };
            }

            return new LoginResponse()
            {
                Error = "Onbekende combinatie"
            };
        }
    }
}
