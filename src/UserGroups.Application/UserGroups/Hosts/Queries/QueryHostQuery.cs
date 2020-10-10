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

namespace UserGroups.Application.UserGroups.Hosts.Queries
{
    [Authorization(ApplicationRoles.Admin)]
    public class QueryHostQuery : SkipTakeRequest, IRequest<SkipTakeSet<HostDto>>
    {
        public string Filter { get; set; }
        public bool IncludeDeleted { get; set; }
    }

    internal class QueryHostQueryHandler : IRequestHandler<QueryHostQuery, SkipTakeSet<HostDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryHostQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SkipTakeSet<HostDto>> Handle(QueryHostQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _dbContext.Hosts
                .ProjectTo<HostDto>(_mapper.ConfigurationProvider)
                .Where(p => request.IncludeDeleted || !p.IsDeleted)
                .Where(p => string.IsNullOrWhiteSpace(request.Filter) ||
                            EF.Functions.Like(p.Name, $"%{request.Filter}%") ||
                            EF.Functions.Like(p.Blurb, $"%{request.Filter}%"))
                .AsSkipTakeSet(request.Skip, request.Take, cancellationToken);

            return result;
        }
    }
}
