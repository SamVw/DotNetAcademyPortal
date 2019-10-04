using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.BL.Services;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.Interfaces;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.ServiceLayer;
using DotNetAcademyPortal.Test.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DotNetAcademyPortal.Test
{
    public class TokenServiceTests
    {
        private IOptions<AppSettings> appSettings;

        private Mock<FakeUserManager> fakeUserManager;

        public TokenServiceTests()
        {
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "Test",
                    Id = Guid.NewGuid().ToString(),
                    Email = "test@test.com"
                }
            }.AsQueryable();

            fakeUserManager = new Mock<FakeUserManager>();
            fakeUserManager.Setup(x => x.Users)
                .Returns(users);
            fakeUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult<IList<string>>(new List<string>() { "Customer" }));

            appSettings = Options.Create<AppSettings>(new AppSettings()
            {
                DaysValid = "5",
                Port = "11111",
                Secret = "testww123456789123456789"
            });
        }

        [Fact]
        public async Task GenerateTokenNotNull()
        {
            var tokenService = new TokenService(fakeUserManager.Object, appSettings);

            var token = await tokenService.GenerateToken(new ApplicationUser()
            {
                UserName = "Test",
                Email = "test@test.com"
            });

            Assert.NotNull(token);
        }

        [Fact]
        public async Task GenerateTokenWithCustomerClaim()
        {
            var tokenService = new TokenService(fakeUserManager.Object, appSettings);

            var token = await tokenService.GenerateToken(new ApplicationUser()
            {
                UserName = "Test",
                Email = "test@test.com"
            });

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);

            Assert.NotNull(jsonToken.Claims.FirstOrDefault(c => c.Value == "Customer"));
        }

        [Fact]
        public async Task GenerateTokenWithAdminClaim()
        {
            fakeUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult<IList<string>>(new List<string>() { "Administrator" }));
            var tokenService = new TokenService(fakeUserManager.Object, appSettings);

            var token = await tokenService.GenerateToken(new ApplicationUser()
            {
                UserName = "Test",
                Email = "test@test.com"
            });

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);

            Assert.NotNull(jsonToken.Claims.FirstOrDefault(c => c.Value == "Administrator"));
        }

        [Fact]
        public async Task GenerateTokenWithCorrectName()
        {
            var tokenService = new TokenService(fakeUserManager.Object, appSettings);

            var token = await tokenService.GenerateToken(new ApplicationUser()
            {
                UserName = "Test",
                Email = "test@test.com"
            });

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);

            Assert.Equal("Test", jsonToken.Claims.FirstOrDefault(c => c.Value == "Test")?.Value);
        }
    }
}
