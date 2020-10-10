using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserGroups.Application.UserGroups.Meetings.Commands
{
    public class UpdateMeetingCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }
        public string ShortBlurb { get; set; }
        public bool IsDeleted { get; set; }
        public bool IncludeInBannerRotation { get; set; }
        public string SponsorUrl { get; set; }
    }

    internal class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand>
    {
        public Task<Unit> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
