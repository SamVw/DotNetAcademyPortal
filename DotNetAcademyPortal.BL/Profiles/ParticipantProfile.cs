using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAcademyPortal.Common.Entities;
using DotNetAcademyPortal.Common.Models;

namespace DotNetAcademyPortal.BL.Profiles
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Participant, ParticipantDto>().ReverseMap();
        }
    }
}
