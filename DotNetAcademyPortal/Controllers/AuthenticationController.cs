using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Auth.Requests;
using DotNetAcademyPortal.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAcademyPortal.ServiceLayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login == null || login.IsEmpty())
            {
                return BadRequest("Invalid data");
            }

            var result = _mediator.Send(new LoginRequest() {Login = login}).Result;

            if (!string.IsNullOrEmpty(result.Token))
            {
                return Ok(result);
            }

            return Unauthorized(result.Error);
        }
    }
}