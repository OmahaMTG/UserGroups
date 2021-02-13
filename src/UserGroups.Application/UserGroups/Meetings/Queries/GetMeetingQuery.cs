using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    [Authorization(ApplicationRoles.Admin)]
    public class GetMeetingQuery : IRequest<MeetingDto>
    {
        public int Id { get; set; }
    }

    internal class GetMeetingQueryHandler : IRequestHandler<GetMeetingQuery, MeetingDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMeetingQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MeetingDto> Handle(GetMeetingQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Meetings
                .ProjectTo<MeetingDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (result == null) throw new NotFoundException("Meetings", request.Id);

            return result;
        }
    }
}
