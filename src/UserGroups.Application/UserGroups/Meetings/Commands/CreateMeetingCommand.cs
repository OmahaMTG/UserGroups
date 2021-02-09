using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Meetings.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class CreateMeetingCommand : IRequest<int>
    {

        public string Title { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MaxRsvp { get; set; }
        public Boolean AllowRsvp { get; set; }
        public string Intro { get; set; }
        public string Footer { get; set; }
        public bool IsDraft { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string VimeoId { get; set; }
        public int? MeetingHostId { get; set; }
        public string MeetingHostBody { get; set; }

    }

    internal class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMeetingCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var newDbMeeting = new Meeting
            {
                AllowRsvp = request.AllowRsvp,
                EndTime = request.EndTime,
                Footer = request.Footer,
                Intro = request.Intro,
                IsDraft = request.IsDraft,
                VimeoId = request.VimeoId,
                MaxRsvp = request.MaxRsvp,
                PublishStartTime = request.PublishStartTime,
                Title = request.Title,
                StartTime = request.StartTime,
                MeetingHostId = request.MeetingHostId,
                HostMeetingBody = request.MeetingHostBody
            };

            await _dbContext.Meetings.AddAsync(newDbMeeting, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newDbMeeting.Id;
        }

    }
}
