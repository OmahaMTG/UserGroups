using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Hosts.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class DeleteHostCommand : IRequest
    {
        public int Id { get; set; }
        public bool HardDelete { get; set; }
    }

    internal class DeleteHostCommandHandler : IRequestHandler<DeleteHostCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteHostCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteHostCommand request, CancellationToken cancellationToken)
        {
            var hostFromDatabase =
                await _dbContext.Hosts.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
            if (hostFromDatabase != null)
            {
                if (request.HardDelete)
                    _dbContext.Hosts.Remove(hostFromDatabase);
                else
                    hostFromDatabase.IsDeleted = true;

                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            throw new NotFoundException("Host", request.Id);
        }
    }
}
