using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Models;

namespace DotNetAcademyPortal.Common.MediatR.Participants.Responses
{
    public class GetParticipantsResponse
    {
        public List<ParticipantDto> Participants { get; set; }
    }
}
