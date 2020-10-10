using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Hosts.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class CreateHostCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }

        public bool IsDeleted { get; set; }

    }

    internal class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateHostCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateHostCommand request, CancellationToken cancellationToken)
        {
            var newDbHost = new Host
            {
                Name = request.Name,
                Blurb = request.Blurb,
                ContactInfo = request.ContactInfo,
                IsDeleted = request.IsDeleted,

            };

            await _dbContext.Hosts.AddAsync(newDbHost, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newDbHost.Id;
        }
    }
}
