using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Meetings.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class UpdateMeetingCommand : IRequest
    {

        public int Id { get; set; }
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

    internal class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateMeetingCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetingToUpdate = await _dbContext.Meetings
                .Include(i => i.MeetingSponsors).ThenInclude(i => i.Sponsor)
                .Include(i => i.MeetingHost)
                .Include(i => i.Presentations).ThenInclude(i => i.PresentationPresenters).ThenInclude(i => i.Presenter)
                .Include(_ => _.MeetingTags).ThenInclude(_ => _.Tag).FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

            if (meetingToUpdate == null)
            {
                throw new NotFoundException("Meeting", request.Id);
            }

            meetingToUpdate.AllowRsvp = request.AllowRsvp;
            meetingToUpdate.EndTime = request.EndTime;
            meetingToUpdate.StartTime = request.StartTime;
            meetingToUpdate.PublishStartTime = request.PublishStartTime;
            meetingToUpdate.Footer = request.Footer;
            meetingToUpdate.Intro = request.Intro;
            meetingToUpdate.IsDraft = request.IsDraft;
            meetingToUpdate.MeetingHostId = request.MeetingHostId;
            meetingToUpdate.HostMeetingBody = request.MeetingHostBody;
            meetingToUpdate.Title = request.Title;
            meetingToUpdate.VimeoId = request.VimeoId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
