using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    public class QueryMeetingQuery : SkipTakeRequest, IRequest<SkipTakeSet<MeetingDto>>
    {
        public int Id { get; set; }
    }

    internal class QueryMeetingQueryHandler : IRequestHandler<QueryMeetingQuery, SkipTakeSet<MeetingDto>>
    {
        public Task<SkipTakeSet<MeetingDto>> Handle(QueryMeetingQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}