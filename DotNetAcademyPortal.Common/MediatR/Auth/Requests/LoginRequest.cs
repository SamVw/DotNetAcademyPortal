﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Dtos;
using DotNetAcademyPortal.Common.MediatR.Auth.Responses;
using MediatR;

namespace DotNetAcademyPortal.Common.MediatR.Auth.Requests
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public Login Login { get; set; }
    }
}
