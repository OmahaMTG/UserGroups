using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Sponsors.Commands
{
    [Authorization(ApplicationRoles.Admin)]
    public class UpdateSponsorCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }
        public string ShortBlurb { get; set; }
        public bool IsDeleted { get; set; }
        public bool IncludeInBannerRotation { get; set; }
        public string SponsorUrl { get; set; }
    }

    internal class UpdateSponsorCommandHandler : IRequestHandler<UpdateSponsorCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateSponsorCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSponsorCommand request, CancellationToken cancellationToken)
        {
            var dbRecord = await _dbContext.Sponsors
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (dbRecord == null) throw new NotFoundException("Sponsor", request.Id);

            dbRecord.Name = request.Name;
            dbRecord.Blurb = request.Blurb;
            dbRecord.ContactInfo = request.ContactInfo;
            dbRecord.ShortBlurb = request.ShortBlurb;
            dbRecord.IsDeleted = request.IsDeleted;
            dbRecord.IncludeInBannerRotation = request.IncludeInBannerRotation;
            dbRecord.SponsorUrl = request.SponsorUrl;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
