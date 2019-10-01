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
    public class GetCustomersRequestHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersRequestHandler(DotNetAcademyPortalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<GetCustomersResponse> IRequestHandler<GetCustomersRequest, GetCustomersResponse>.Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.ProjectTo<CustomerDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return new GetCustomersResponse() { Customers = customers };
        }
    }
}
