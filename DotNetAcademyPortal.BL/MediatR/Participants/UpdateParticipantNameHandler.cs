using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Participants.Requests;
using DotNetAcademyPortal.Common.MediatR.Participants.Responses;
using DotNetAcademyPortal.DAL;
using MediatR;

namespace DotNetAcademyPortal.BL.MediatR.Participants
{
    public class UpdateParticipantNameHandler : IRequestHandler<UpdateParticipantNameRequest, UpdateParticipantNameResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;

        public UpdateParticipantNameHandler(DotNetAcademyPortalDbContext context)
        {
            _context = context;
        }

        async Task<UpdateParticipantNameResponse> IRequestHandler<UpdateParticipantNameRequest, UpdateParticipantNameResponse>.Handle(UpdateParticipantNameRequest request, CancellationToken cancellationToken)
        {
            if (!_context.Customers.Any(c => c.CustomerId == request.CustomerId))
            {
                return new UpdateParticipantNameResponse() { Error = "Geen bestaande klant gevonden" };
            }

            if (!_context.Participants.Any(c => c.Id == request.Participant.Id))
            {
                return new UpdateParticipantNameResponse() { Error = "Deelnemer niet gevonden" };
            }

            var participant = _context.Participants.First(c => c.Id == request.Participant.Id);
            participant.Name = request.Participant.Name;

            _context.Participants.Update(participant);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateParticipantNameResponse();
        }
    }
}
