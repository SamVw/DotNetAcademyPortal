using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Participants.Requests;
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
    public class ParticipantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParticipantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("customer/{id}/name")]
        public async Task<IActionResult> PutParticipantNameAsync(string id, [FromBody] ParticipantDto participant)
        {
            var result = await _mediator.Send(new UpdateParticipantNameRequest()
            {
                Participant = participant,
                CustomerId = id,
                UserName = User.Identity.Name
            });
            if (result.Error != null)
            {
                return Unauthorized(result.Error);
            }

            return Ok();
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer/authenticated/{id}")]
        public async Task<IActionResult> GetAuthenticatedAsync(string id)
        {
            var result = await _mediator.Send(new GetParticipantsForAuthCustomerRequest()
                {RouteId = id, UserName = User.Identity.Name});
            if (result.Error != null)
            {
                Unauthorized(result.Error);
            }

            return Ok(result.Participants);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetParticipantsAsync(string id)
        {
            var result = await _mediator.Send(new GetParticpantsRequest() {CustomerId = id});

            return Ok(result.Participants);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("customer/{id}")]
        public async Task<IActionResult> PostParticipantAsync(string id, [FromBody] ParticipantDto participant)
        {
            var result = await _mediator.Send(new CreateParticipantRequest() {CustomerId = id, Participant = participant});
            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Participant);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("customer/{id}")]
        public async Task<IActionResult> PutParticipantAsync(string id, [FromBody] ParticipantDto participant)
        {
            var result = await _mediator.Send(new UpdateParticipantRequest() { CustomerId = id, Participant = participant });
            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Participant);
        }
    }
}