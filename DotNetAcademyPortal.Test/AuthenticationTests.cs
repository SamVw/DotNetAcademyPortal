using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNetAcademyPortal.BL.MediatR.Auth;
using DotNetAcademyPortal.BL.Services;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.MediatR.Auth.Requests;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using DotNetAcademyPortal.Test.Helpers;
using DotNetAcademyPortal.Test.Mocks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DotNetAcademyPortal.Test
{
    public class AuthenticationTests
    {
        private Mock<FakeUserManager> fakeUserManager;

        private Mock<IDataContext> context;

        private TokenService tokenService;

        public AuthenticationTests()
        {
            // Fake user manager
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "Test",
                    Id = "test id",
                    Email = "test@test.com"
                }
            }.AsQueryable();
            fakeUserManager = new Mock<FakeUserManager>();
            fakeUserManager.Setup(x => x.Users)
                .Returns(users);
            fakeUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(new ApplicationUser() { UserName = "Test" }));
            fakeUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult<IList<string>>(new List<string>() { "Customer" }));

            // Token service
            var appSettings = Options.Create<AppSettings>(new AppSettings()
            {
                DaysValid = "5",
                Port = "11111",
                Secret = "testww123456789123456789"
            });
            tokenService = new TokenService(fakeUserManager.Object, appSettings);

            // Data context
            List<Customer> customers = new List<Customer>()
            {
                new Customer()
                {
                    CustomerId = "test id",
                    Name = "Test",
                    Address = "test 11",
                    MaxAllowedParticipants = 5,
                    ApplicationUser = new ApplicationUser()
                    {
                        Id = "test id",
                        UserName = "Test"
                    }
                }
            };
            var cs = DbSetHelper.CreateDbSetMock(customers.AsEnumerable());
            context = new Mock<IDataContext>();
            context.Setup(x => x.Customers)
                .Returns(cs.Object);
        }

        [Fact]
        public async void ValidLoginForCustomer()
        {
            fakeUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Administrator"))
                .Returns(Task.FromResult<bool>(false));
            fakeUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult<bool>(true));

            LoginRequest request = new LoginRequest() { Login = new Login() { UserName = "Test", Password = "Test"} };
            LoginRequestHandler handler = new LoginRequestHandler(tokenService, fakeUserManager.Object, context.Object);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.Equal("Test", response.UserName);
            Assert.False(response.IsAdmin);
        }

        [Fact]
        public async void ValidLoginForAdministrator()
        {
            fakeUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Administrator"))
                .Returns(Task.FromResult<bool>(true));
            fakeUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult<bool>(true));

            LoginRequest request = new LoginRequest() { Login = new Login() { UserName = "Test", Password = "Test" } };
            LoginRequestHandler handler = new LoginRequestHandler(tokenService, fakeUserManager.Object, context.Object);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.Equal("Test", response.UserName);
            Assert.True(response.IsAdmin);
        }

        [Fact]
        public async void InvalidLogin()
        {
            LoginRequest request = new LoginRequest() { Login = new Login() { UserName = "Does not exist", Password = "wrong..." } };
            LoginRequestHandler handler = new LoginRequestHandler(tokenService, fakeUserManager.Object, context.Object);

            var response = await handler.Handle(request, new CancellationToken());

            Assert.NotNull(response.Error);
        }
    }
}
