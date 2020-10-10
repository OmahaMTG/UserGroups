using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserGroups.Application.Common.Behaviours;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.UserGroups.Sponsors.Queries
{
    [Authorization(ApplicationRoles.Admin)]
    public class QuerySponsorQuery : SkipTakeRequest, IRequest<SkipTakeSet<SponsorDto>>
    {
        public string Filter { get; set; }
        public bool IncludeDeleted { get; set; }
    }

    internal class QuerySponsorQueryHandler : IRequestHandler<QuerySponsorQuery, SkipTakeSet<SponsorDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuerySponsorQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SkipTakeSet<SponsorDto>> Handle(QuerySponsorQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _dbContext.Sponsors
                .ProjectTo<SponsorDto>(_mapper.ConfigurationProvider)
                .Where(p => request.IncludeDeleted || !p.IsDeleted)
                .Where(p => string.IsNullOrWhiteSpace(request.Filter) ||
                            EF.Functions.Like(p.Name, $"%{request.Filter}%") ||
                            EF.Functions.Like(p.Blurb, $"%{request.Filter}%") ||
                            EF.Functions.Like(p.ShortBlurb, $"%{request.Filter}%"))
                .AsSkipTakeSet(request.Skip, request.Take, cancellationToken);

            return result;
        }
    }
}
