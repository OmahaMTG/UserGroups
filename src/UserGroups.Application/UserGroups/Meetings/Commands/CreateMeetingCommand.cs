using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public CreateMeetingCommand()
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
                HostMeetingBody = request.MeetingHostBody,
                Intro = request.Intro,
                IsDraft = request.IsDraft,
                VimeoId = request.VimeoId,
                MaxRsvp = request.MaxRsvp,
                PublishStartTime = request.PublishStartTime,
                Title = request.Title,
                StartTime = request.StartTime,
                MeetingHostId = request.MeetingHostId,
                MeetingSponsors = request.MeetingSponsors?.Select(ms => new MeetingSponsor { SponsorId = ms.SponsorId, MeetingSponsorBody = ms.Body }).ToList(),
                Presentations = request.MeetingPresentations?.Select(ToPresentationData).ToList()
            };

            await _dbContext.Meetings.AddAsync(newDbMeeting, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newDbMeeting.Id;
        }

        private static Presentation ToPresentationData(CreateMeetingCommand.MeetingPresentation meeting)
        {
            return new Presentation()
            {
                VimeoId = meeting.VimeoId,
                Details = meeting.Body,
                Title = meeting.Title,
                PresentationPresenters = meeting.MeetingPresentationPresenters?.Select(ToPresentationPresenterData).ToList()
            };
        }

        private static PresentationPresenter ToPresentationPresenterData(CreateMeetingCommand.MeetingPresentationPresenter presenter)
        {
            return new PresentationPresenter()
            {
                PresenterId = presenter.PresenterId,
                PresenterPresentationBody = presenter.Body
            };
        }
    }
}
