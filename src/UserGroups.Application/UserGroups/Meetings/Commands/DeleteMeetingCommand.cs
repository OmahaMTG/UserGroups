using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Extensions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Meetings.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class DeleteMeetingCommand : IRequest
    {
        public int Id { get; set; }
        public bool HardDelete { get; set; }
    }

    internal class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteMeetingCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetingFromDatabase =
                await _dbContext.Meetings
                    .Include(m => m.Presentations)
                    .ThenInclude(p => p.PresentationPresenters)
                    .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (meetingFromDatabase == null)
            {
                throw new NotFoundException("Meeting", request.Id);
            }

            if (request.HardDelete)
            {
                _dbContext.PresentationPresenters.RemoveWhere(p => p.Presentation.MeetingId == request.Id);
                _dbContext.Presentations.RemoveWhere(p => p.MeetingId == request.Id);
                _dbContext.MeetingSponsors.RemoveWhere(ms => ms.MeetingId == request.Id);
                _dbContext.MeetingTags.RemoveWhere(t => t.MeetingId == request.Id);
                _dbContext.MeetingRsvps.RemoveWhere(rsvp => rsvp.MeetingId == request.Id);

                _dbContext.Meetings.Remove(meetingFromDatabase);
            }
            else
            {
                meetingFromDatabase.IsDeleted = true;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }







    }
}
