﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.MediatR.Customers.Requests;
using DotNetAcademyPortal.Common.MediatR.Customers.Responses;
using DotNetAcademyPortal.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotNetAcademyPortal.BL.MediatR.Auth
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly DotNetAcademyPortalDbContext _context;

        public CreateCustomerRequestHandler(UserManager<ApplicationUser> userManager, IMapper mapper, DotNetAcademyPortalDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        async Task<CreateCustomerResponse> IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>.Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
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
                _context.Customers.Add(c);
                await _context.SaveChangesAsync(cancellationToken);

                return new CreateCustomerResponse();
            }

            return new CreateCustomerResponse() { Error = result.Errors.Aggregate("", (current, next) => current += next.Description) };
        }
    }
}
