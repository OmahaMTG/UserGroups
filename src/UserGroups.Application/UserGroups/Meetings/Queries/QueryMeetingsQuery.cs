using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    public class QueryMeetingsQuery : SkipTakeRequest, IRequest<SkipTakeSet<MeetingDto>>
    {
        public string Filter { get; set; }
        public bool IncludeDeleted { get; set; }
    }

    internal class QueryMeetingsQueryHandler : IRequestHandler<QueryMeetingsQuery, SkipTakeSet<MeetingDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryMeetingsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SkipTakeSet<MeetingDto>> Handle(QueryMeetingsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Meetings
                .ProjectTo<MeetingDto>(_mapper.ConfigurationProvider)
                .Where(p => request.IncludeDeleted || !p.IsDeleted)
                .Where(p => string.IsNullOrWhiteSpace(request.Filter) ||
                            EF.Functions.Like(p.Title, $"%{request.Filter}%") ||
                            EF.Functions.Like(p.Intro, $"%{request.Filter}%"))
                .AsSkipTakeSet(request.Skip, request.Take, cancellationToken);

            return result;
        }
    }
}
