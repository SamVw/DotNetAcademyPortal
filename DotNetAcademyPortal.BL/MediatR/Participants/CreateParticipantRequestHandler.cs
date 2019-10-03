using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.MediatR.Participants.Requests;
using DotNetAcademyPortal.Common.MediatR.Participants.Responses;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using MediatR;

namespace DotNetAcademyPortal.BL.MediatR.Participants
{
    public class CreateParticipantRequestHandler : IRequestHandler<CreateParticipantRequest, CreateParticipantResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly IMapper _mapper;

        public CreateParticipantRequestHandler(DotNetAcademyPortalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<CreateParticipantResponse> IRequestHandler<CreateParticipantRequest, CreateParticipantResponse>.Handle(CreateParticipantRequest request, CancellationToken cancellationToken)
        {
            if (!_context.Customers.Any(c => c.CustomerId == request.CustomerId))
            {
                return new CreateParticipantResponse() { Error = "Geen bestaande klant gevonden" };
            }

            var currentParticipantsCount = _context.Participants.Count(p => p.CustomerId == request.CustomerId);
            var maxAllowedParticipants = _context.Customers.First(c => c.CustomerId == request.CustomerId).MaxAllowedParticipants;
            if (currentParticipantsCount >= maxAllowedParticipants)
            {
                return new CreateParticipantResponse() { Error = "Maximum aantal deelnemers voor klant bereikt" };
            }

            var participant = _mapper.Map<Participant>(request.Participant);
            participant.CustomerId = request.CustomerId;
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateParticipantResponse() { Participant = _mapper.Map<ParticipantDto>(participant)};
        }
    }
}
