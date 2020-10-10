using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    public class GetMeetingQuery : IRequest<MeetingDto>
    {
        public int Id { get; set; }
    }

    internal class GetMeetingQueryHandler : IRequestHandler<GetMeetingQuery, MeetingDto>
    {
        public Task<MeetingDto> Handle(GetMeetingQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}