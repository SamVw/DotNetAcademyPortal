using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.Interfaces;
using DotNetAcademyPortal.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DotNetAcademyPortal.BL.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;

        public TokenService(UserManager<ApplicationUser> userManager, IOptions<AppSettings> options)
        {
            _userManager = userManager;
            _appSettings = options.Value;
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            foreach (string item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:" + _appSettings.Port,
                audience: "http://localhost:" + _appSettings.Port,
                claims: claims,
                expires: DateTime.Now.AddDays(double.Parse(_appSettings.DaysValid)),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }
    }
}
