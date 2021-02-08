using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Meetings.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class UpdateMeetingCommand : IRequest
    {
        public UpdateMeetingCommand()
        {
            MeetingSponsors = new List<MeetingSponsor>();
            MeetingPresentations = new List<MeetingPresentation>();
        }
        public class MeetingSponsor
        {
            public int SponsorId { get; set; }
            public string Body { get; set; }
        }

        public class MeetingPresentation
        {
            public int? Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public string VimeoId { get; set; }
            public IEnumerable<MeetingPresentationPresenter> MeetingPresentationPresenters { get; set; }
        }

        public class MeetingPresentationPresenter
        {
            public int PresenterId { get; set; }
            public string Body { get; set; }
        }

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
        public IList<MeetingSponsor> MeetingSponsors { get; set; }
        public int? MeetingHostId { get; set; }
        public string MeetingHostBody { get; set; }
        public IList<MeetingPresentation> MeetingPresentations { get; set; }
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

            meetingToUpdate.MeetingSponsors = request.MeetingSponsors.Select(ms => new MeetingSponsor()
            { SponsorId = ms.SponsorId, MeetingSponsorBody = ms.Body }).ToList();

            meetingToUpdate.Presentations = request.MeetingPresentations.Select(mp => new Presentation()
            {
                Id = mp.Id ??= 0,
                Title = mp.Title,
                Details = mp.Body,
                VimeoId = mp.VimeoId
            }).ToList();


            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
