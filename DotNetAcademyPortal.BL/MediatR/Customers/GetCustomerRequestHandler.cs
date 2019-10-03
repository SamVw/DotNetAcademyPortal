using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotNetAcademyPortal.Common.MediatR.Customers.Requests;
using DotNetAcademyPortal.Common.MediatR.Customers.Responses;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DotNetAcademyPortal.BL.MediatR.Customers
{
    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerRequestHandler(DotNetAcademyPortalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<GetCustomerResponse> IRequestHandler<GetCustomerRequest, GetCustomerResponse>.Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.ProjectTo<CustomerDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (customer == null)
            {
                return  new GetCustomerResponse();
            }

            return new GetCustomerResponse() { Customer = customer };
        }
    }
}
