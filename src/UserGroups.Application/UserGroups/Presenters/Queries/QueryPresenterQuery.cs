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

namespace UserGroups.Application.UserGroups.Presenters.Queries
{
    [Authorization(ApplicationRoles.Admin)]
    public class QueryPresenterQuery : SkipTakeRequest, IRequest<SkipTakeSet<PresenterDto>>
    {
        public string Filter { get; set; }
        public bool IncludeDeleted { get; set; }
    }

    internal class QueryPresenterQueryHandler : IRequestHandler<QueryPresenterQuery, SkipTakeSet<PresenterDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryPresenterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SkipTakeSet<PresenterDto>> Handle(QueryPresenterQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _dbContext.Presenters
                .ProjectTo<PresenterDto>(_mapper.ConfigurationProvider)
                .Where(p => request.IncludeDeleted || !p.IsDeleted)
                .Where(p => string.IsNullOrWhiteSpace(request.Filter) ||
                            EF.Functions.Like(p.Name, $"%{request.Filter}%") ||
                            EF.Functions.Like(p.Bio, $"%{request.Filter}%"))
                .AsSkipTakeSet(request.Skip, request.Take, cancellationToken);

            return result;
        }
    }
}
