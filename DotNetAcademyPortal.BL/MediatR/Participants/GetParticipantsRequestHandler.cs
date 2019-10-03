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
using Microsoft.EntityFrameworkCore;

namespace DotNetAcademyPortal.BL.MediatR.Participants
{
    public class GetParticipantsRequestHandler : IRequestHandler<GetParticpantsRequest, GetParticipantsResponse>
    {
        private readonly DotNetAcademyPortalDbContext _context;
        private readonly IMapper _mapper;

        public GetParticipantsRequestHandler(DotNetAcademyPortalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<GetParticipantsResponse> IRequestHandler<GetParticpantsRequest, GetParticipantsResponse>.Handle(GetParticpantsRequest request, CancellationToken cancellationToken)
        {
            var participants = await _context.Participants.Where(p => p.CustomerId == request.CustomerId).ProjectTo<ParticipantDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return new GetParticipantsResponse() { Participants = participants };
        }
    }
}
