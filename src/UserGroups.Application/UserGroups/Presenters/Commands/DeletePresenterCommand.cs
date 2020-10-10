using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Presenters.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class DeletePresenterCommand : IRequest
    {
        public int Id { get; set; }
        public bool HardDelete { get; set; }
    }

    internal class DeletePresenterCommandHandler : IRequestHandler<DeletePresenterCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeletePresenterCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeletePresenterCommand request, CancellationToken cancellationToken)
        {
            var presenterFromDatabase =
                await _dbContext.Presenters.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
            if (presenterFromDatabase != null)
            {
                if (request.HardDelete)
                    _dbContext.Presenters.Remove(presenterFromDatabase);
                else
                    presenterFromDatabase.IsDeleted = true;

                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            throw new NotFoundException("Presenter", request.Id);
        }
    }
}
