using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Hosts.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class UpdateHostCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }
        public bool IsDeleted { get; set; }
    }

    internal class UpdateHostCommandHandler : IRequestHandler<UpdateHostCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateHostCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateHostCommand request, CancellationToken cancellationToken)
        {
            var dbRecord = await _dbContext.Hosts
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (dbRecord == null)
            {
                throw new NotFoundException("Hosts", request.Id);
            }

            dbRecord.Name = request.Name;
            dbRecord.Blurb = request.Blurb;
            dbRecord.ContactInfo = request.ContactInfo;
            dbRecord.IsDeleted = request.IsDeleted;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
