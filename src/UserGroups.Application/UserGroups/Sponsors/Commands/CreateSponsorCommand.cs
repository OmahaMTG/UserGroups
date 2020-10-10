using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Sponsors.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class CreateSponsorCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }
        public string ShortBlurb { get; set; }
        public bool IsDeleted { get; set; }
        public bool IncludeInBannerRotation { get; set; }
        public string SponsorUrl { get; set; }
    }

    internal class CreateSponsorCommandHandler : IRequestHandler<CreateSponsorCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateSponsorCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSponsorCommand request, CancellationToken cancellationToken)
        {
            var newDbSponsor = new Sponsor
            {
                Blurb = request.Blurb,
                ShortBlurb = request.ShortBlurb,
                ContactInfo = request.ContactInfo,
                IncludeInBannerRotation = request.IncludeInBannerRotation,
                IsDeleted = request.IsDeleted,
                Name = request.Name,
                SponsorUrl = request.SponsorUrl
            };

            await _dbContext.Sponsors.AddAsync(newDbSponsor, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newDbSponsor.Id;
        }
    }
}
