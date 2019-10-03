using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Participants.Responses;
using DotNetAcademyPortal.Common.Models;
using MediatR;

namespace DotNetAcademyPortal.Common.MediatR.Participants.Requests
{
    public class CreateParticipantRequest : IRequest<CreateParticipantResponse>
    {
        public string CustomerId { get; set; }

        public ParticipantDto Participant { get; set; }
    }
}
