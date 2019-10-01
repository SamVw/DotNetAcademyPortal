using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Models;

namespace DotNetAcademyPortal.Common.MediatR.Customers.Responses
{
    public class UpdateCustomerResponse
    {
        public string Error { get; set; }

        public CustomerDto Customer { get; set; }
    }
}
