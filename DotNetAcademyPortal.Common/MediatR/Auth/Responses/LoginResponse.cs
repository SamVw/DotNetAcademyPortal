using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAcademyPortal.Common.MediatR.Auth.Responses
{
    public class LoginResponse
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public bool IsAdmin { get; set; }

        public string Error { get; set; }
    }
}
