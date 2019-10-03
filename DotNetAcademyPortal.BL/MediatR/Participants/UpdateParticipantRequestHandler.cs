using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.Common.MediatR.Participants.Requests;
using DotNetAcademyPortal.Common.MediatR.Participants.Responses;
using DotNetAcademyPortal.Common.Models;
using DotNetAcademyPortal.DAL;
using MediatR;

namespace DotNetAcademyPortal.BL.MediatR.Participants
{
    public class UpdateParticipantRequestHandler : IRequestHandler<UpdateParticipantRequest, UpdateParticipantResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly IMapper _mapper;

        public UpdateParticipantRequestHandler(DotNetAcademyPortalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<UpdateParticipantResponse> IRequestHandler<UpdateParticipantRequest, UpdateParticipantResponse>.Handle(UpdateParticipantRequest request, CancellationToken cancellationToken)
        {
            if (!_context.Customers.Any(c => c.CustomerId == request.CustomerId))
            {
                return new UpdateParticipantResponse() { Error = "Geen bestaande klant gevonden" };
            }

            if (!_context.Participants.Any(c => c.Id == request.Participant.Id))
            {
                return new UpdateParticipantResponse() { Error = "Deelnemer niet gevonden" };
            }

            var participant = _context.Participants.First(c => c.Id == request.Participant.Id);
            participant.Email = request.Participant.Email;
            participant.Name = request.Participant.Name;
            participant.StartDate = request.Participant.StartDate;
            participant.EndDate = request.Participant.EndDate;

            _context.Participants.Update(participant);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateParticipantResponse() { Participant = _mapper.Map<ParticipantDto>(participant) };
        }
    }
}
