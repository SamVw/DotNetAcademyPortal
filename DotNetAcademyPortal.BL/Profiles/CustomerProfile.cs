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
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>();

            CreateMap<Customer, CustomerDto>()
                .ForMember(c => c.Email, opt => opt.MapFrom(c => c.ApplicationUser.Email))
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.ApplicationUser.Id));
        }
    }
}
