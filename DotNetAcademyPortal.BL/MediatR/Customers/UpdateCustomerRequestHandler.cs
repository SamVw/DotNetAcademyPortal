using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.EntityFrameworkCore;

namespace DotNetAcademyPortal.BL.MediatR.Customers
{
    public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, UpdateCustomerResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateCustomerRequestHandler(DotNetAcademyPortalDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        async Task<UpdateCustomerResponse> IRequestHandler<UpdateCustomerRequest, UpdateCustomerResponse>.Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Include(c => c.ApplicationUser).FirstAsync(c => c.CustomerId == request.Customer.Id, cancellationToken);

            customer.ApplicationUser.Email = request.Customer.Email;
            customer.ApplicationUser.UserName = request.Customer.Email;
            var result = await _userManager.UpdateAsync(customer.ApplicationUser);
            if (!result.Succeeded)
            {
                return new UpdateCustomerResponse() { Error = "Email al aanwezig in het systeem" };
            }

            customer.Name = request.Customer.Name;
            customer.Address = request.Customer.Address;
            customer.MaxAllowedParticipants = request.Customer.MaxAllowedParticipants;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateCustomerResponse() { Customer = _mapper.Map<CustomerDto>(customer) };
        }
    }
}
