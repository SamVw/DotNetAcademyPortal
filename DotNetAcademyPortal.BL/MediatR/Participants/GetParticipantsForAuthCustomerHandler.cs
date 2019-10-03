using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.MediatR.Participants.Requests;
using DotNetAcademyPortal.Common.MediatR.Participants.Responses;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotNetAcademyPortal.BL.MediatR.Participants
{
    public class GetParticipantsForAuthCustomerHandler : IRequestHandler<GetParticipantsForAuthCustomerRequest, GetParticipantsForAuthCustomerResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetParticipantsForAuthCustomerHandler(DotNetAcademyPortalDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        async Task<GetParticipantsForAuthCustomerResponse> IRequestHandler<GetParticipantsForAuthCustomerRequest, GetParticipantsForAuthCustomerResponse>.Handle(GetParticipantsForAuthCustomerRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user.Id != request.RouteId)
            {
                return new GetParticipantsForAuthCustomerResponse() { Error = "Foute authenticatie gegevens" };
            }

            var participants = await _context.Participants.Where(p => p.CustomerId == request.RouteId).ProjectTo<ParticipantDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return new GetParticipantsForAuthCustomerResponse() { Participants = participants };
        }
    }
}
