using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Customers.Responses;
using DotNetAcademyPortal.Common.Models;
using MediatR;

namespace DotNetAcademyPortal.Common.MediatR.Customers.Requests
{
    public class CreateCustomerRequest : IRequest<CreateCustomerResponse>
    {
        public CustomerDto Customer { get; set; }
    }
}
