using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.MediatR.Customers.Requests;
using DotNetAcademyPortal.Common.MediatR.Customers.Responses;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DotNetAcademyPortal.BL.MediatR.Customers
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IDataContext _context;

        public CreateCustomerRequestHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IDataContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer c = _mapper.Map<Customer>(request.Customer);
            c.ApplicationUser = new ApplicationUser()
            {
                Email = request.Customer.Email,
                UserName = request.Customer.Email
            };

            if (await _userManager.FindByEmailAsync(c.ApplicationUser.Email) != null)
            {
                return new CreateCustomerResponse() { Error = "Email is al aanwezig in het systeem" };
            }

            var result = await _userManager.CreateAsync(c.ApplicationUser, "@Test123");
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(c.ApplicationUser, "Customer");
                _context.Customers.Add(c);
                var r = await _context.SaveChangesAsync();

                return new CreateCustomerResponse() { Customer = _mapper.Map<CustomerDto>(c) };
            }

            return new CreateCustomerResponse() { Error = result.Errors.Aggregate("", (current, next) => current += next.Description) };
        }
    }
}
