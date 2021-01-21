//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Behaviours;
//using UserGroups.Application.Common.Interfaces;
//using UserGroups.Application.Common.Models;

//namespace UserGroups.Application.UserGroups.Meetings.Commands
//{
//    [Authorization(ApplicationRoles.Admin)]
//    public class UpdateMeetingCommand : IRequest<int>
//    {
//        public UpdateMeetingCommand()
//        {
//            MeetingSponsors = new List<MeetingSponsor>();
//            MeetingPresentations = new List<MeetingPresentation>();
//        }
//        public class MeetingSponsor
//        {
//            public int SponsorId { get; set; }
//            public string Body { get; set; }
//        }

//        public class MeetingPresentation
//        {
//            public string Title { get; set; }
//            public string Body { get; set; }
//            public string VimeoId { get; set; }
//            public IEnumerable<MeetingPresentationPresenter> MeetingPresentationPresenters { get; set; }
//        }

//        public class MeetingPresentationPresenter
//        {
//            public int PresenterId { get; set; }
//            public string Body { get; set; }
//        }

//        public int Id { get; set; }
//        public string Title { get; set; }
//        public DateTime? PublishStartTime { get; set; }
//        public DateTime? StartTime { get; set; }
//        public DateTime? EndTime { get; set; }
//        public int? MaxRsvp { get; set; }
//        public Boolean AllowRsvp { get; set; }
//        public string Intro { get; set; }
//        public string Footer { get; set; }
//        public bool IsDraft { get; set; }
//        public IEnumerable<string> Tags { get; set; }
//        public string VimeoId { get; set; }
//        public IList<MeetingSponsor> MeetingSponsors { get; set; }
//        public int? MeetingHostId { get; set; }
//        public string MeetingHostBody { get; set; }
//        public IList<MeetingPresentation> MeetingPresentations { get; set; }
//    }

//    internal class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand>
//    {
//        private readonly IApplicationDbContext _dbContext;

//        public UpdateMeetingCommandHandler(IApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public Task<Unit> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
//        {

//            throw new NotImplementedException();
//        }
//    }
//}
