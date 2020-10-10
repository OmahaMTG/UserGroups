using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Presenters.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class CreatePresenterCommand : IRequest<int>
    {

        public string Name { get; set; }
        public string Bio { get; set; }
        public string ContactInfo { get; set; }
        public string OmahaMtgUserId { get; set; }
        public bool IsDeleted { get; set; }

    }

    internal class CreatePresenterCommandHandler : IRequestHandler<CreatePresenterCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreatePresenterCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePresenterCommand request, CancellationToken cancellationToken)
        {
            var newDbPresenter = new Presenter
            {
                Name = request.Name,
                Bio = request.Bio,
                ContactInfo = request.ContactInfo,
                OmahaMtgUserId = request.OmahaMtgUserId,
                IsDeleted = request.IsDeleted
            };

            await _dbContext.Presenters.AddAsync(newDbPresenter, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newDbPresenter.Id;
        }
    }
}
