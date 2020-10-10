using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UserGroups.Application.UserGroups.Meetings.Commands
{
    public class DeleteMeetingCommand : IRequest
    {
        public int Id { get; set; }
    }

    internal class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand>
    {
        public Task<Unit> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}