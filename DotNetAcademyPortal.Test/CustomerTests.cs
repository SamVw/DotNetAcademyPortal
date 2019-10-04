using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.BL.MediatR.Customers;
using DotNetAcademyPortal.BL.Profiles;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.MediatR.Customers.Requests;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using DotNetAcademyPortal.Test.Helpers;
using DotNetAcademyPortal.Test.Mocks;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DotNetAcademyPortal.Test
{
    
    public class CustomerTests
    {
        private Mock<FakeUserManager> fakeUserManager;

        private Mock<IDataContext> context;

        private IMapper mapper;

        public CustomerTests()
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

            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>()));
        }

        [Fact]
        public async void CreateCustomerWithExistingEmail()
        {
            var customer = new CustomerDto()
            {
                Email = "test@test.com",
                Name = "Test",
                Address = "test 11",
                MaxAllowedParticipants = 5
            };

            var request = new CreateCustomerRequest() { Customer = customer };
            var handler = new CreateCustomerRequestHandler(fakeUserManager.Object, mapper, context.Object);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(response.Error);
        }
    }
}
