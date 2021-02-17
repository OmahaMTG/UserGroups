using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Presenters.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class UpdatePresenterCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ContactInfo { get; set; }
        public string OmahaMtgUserId { get; set; }
        public bool IsDeleted { get; set; }
    }

    internal class UpdatePresenterCommandHandler : IRequestHandler<UpdatePresenterCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdatePresenterCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdatePresenterCommand request, CancellationToken cancellationToken)
        {
            var dbRecord = await _dbContext.Presenters
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (dbRecord == null) throw new NotFoundException("Presenter", request.Id);

            dbRecord.Name = request.Name;
            dbRecord.ContactInfo = request.ContactInfo;
            dbRecord.IsDeleted = request.IsDeleted;
            dbRecord.Bio = request.Bio;
            dbRecord.OmahaMtgUserId = request.OmahaMtgUserId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
