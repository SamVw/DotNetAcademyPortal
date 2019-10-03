using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.MediatR.Participants.Responses;
using MediatR;

namespace DotNetAcademyPortal.Common.MediatR.Participants.Requests
{
    public class GetParticpantsRequest : IRequest<GetParticipantsResponse>
    {
        public string CustomerId { get; set; }
    }
}
