using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GetAuthenticatedCustomerHandler : IRequestHandler<GetAuthenticatedCustomerRequest, GetAuthenticatedCustomerResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAuthenticatedCustomerHandler(DotNetAcademyPortalDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        async Task<GetAuthenticatedCustomerResponse> IRequestHandler<GetAuthenticatedCustomerRequest, GetAuthenticatedCustomerResponse>.Handle(GetAuthenticatedCustomerRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user.Id != request.RouteId)
            {
                return new GetAuthenticatedCustomerResponse() { Error = "Foute authenticatie gegevens" };
            }

            var customer = await _context.Customers.Include(c => c.ApplicationUser)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider).FirstAsync(c => c.Id == request.RouteId, cancellationToken);

            return  new GetAuthenticatedCustomerResponse() { Customer = customer };
        }
    }
}
