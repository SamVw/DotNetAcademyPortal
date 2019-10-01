using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Customers.Requests;
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
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] CustomerDto customer)
        {
            var result = await _mediator.Send(new UpdateCustomerRequest() {Customer = customer});

            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Customer);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetCustomersRequest());
            return Ok(result.Customers);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customer)
        {
            if (customer == null)
            {
                return BadRequest("Provide a valid input");
            }

            var result = await _mediator.Send(new CreateCustomerRequest() {Customer = customer});

            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}