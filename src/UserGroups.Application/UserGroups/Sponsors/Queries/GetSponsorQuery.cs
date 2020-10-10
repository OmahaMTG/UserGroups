using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Sponsors.Queries
{
    [Authorization(ApplicationRoles.Admin)]
    public class GetSponsorQuery : IRequest<SponsorDto>
    {
        public int Id { get; set; }
    }

    internal class GetSponsorQueryHandler : IRequestHandler<GetSponsorQuery, SponsorDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSponsorQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SponsorDto> Handle(GetSponsorQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Sponsors
                .ProjectTo<SponsorDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (result == null) throw new NotFoundException("Sponsor", request.Id);

            return result;
        }
    }
}
