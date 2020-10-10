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

namespace UserGroups.Application.UserGroups.Presenters.Queries
{
    [Authorization(ApplicationRoles.Admin)]
    public class GetPresenterQuery : IRequest<PresenterDto>
    {
        public int Id { get; set; }
    }

    internal class GetPresenterQueryHandler : IRequestHandler<GetPresenterQuery, PresenterDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPresenterQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PresenterDto> Handle(GetPresenterQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Presenters
                .ProjectTo<PresenterDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (result == null) throw new NotFoundException("Presenters", request.Id);

            return result;
        }
    }
}
