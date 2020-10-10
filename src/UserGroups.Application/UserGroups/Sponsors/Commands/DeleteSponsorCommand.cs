using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Sponsors.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class DeleteSponsorCommand : IRequest
    {
        public int Id { get; set; }
        public bool HardDelete { get; set; }
    }

    internal class DeleteSponsorCommandHandler : IRequestHandler<DeleteSponsorCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteSponsorCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteSponsorCommand request, CancellationToken cancellationToken)
        {
            var sponsorFromDatabase =
                await _dbContext.Sponsors.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
            if (sponsorFromDatabase != null)
            {
                if (request.HardDelete)
                    _dbContext.Sponsors.Remove(sponsorFromDatabase);
                else
                    sponsorFromDatabase.IsDeleted = true;

                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            throw new NotFoundException("Sponsor", request.Id);
        }
    }
}
