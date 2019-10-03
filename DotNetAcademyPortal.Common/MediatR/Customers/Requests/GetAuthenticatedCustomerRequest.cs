using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Customers.Responses;
using MediatR;

namespace DotNetAcademyPortal.Common.MediatR.Customers.Requests
{
    public class GetAuthenticatedCustomerRequest : IRequest<GetAuthenticatedCustomerResponse>
    {
        public string RouteId { get; set; }

        public string UserName { get; set; }
    }
}
