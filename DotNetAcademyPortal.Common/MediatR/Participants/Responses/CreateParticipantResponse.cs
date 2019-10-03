using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.Models;

namespace DotNetAcademyPortal.Common.MediatR.Participants.Responses
{
    public class CreateParticipantResponse
    {
        public string Error { get; set; }

        public ParticipantDto Participant { get; set; }
    }
}
