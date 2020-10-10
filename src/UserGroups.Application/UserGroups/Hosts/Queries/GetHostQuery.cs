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

namespace UserGroups.Application.UserGroups.Hosts.Queries
{
    [Authorization(ApplicationRoles.Admin)]
    public class GetHostQuery : IRequest<HostDto>
    {
        public int Id { get; set; }
    }

    internal class GetHostQueryHandler : IRequestHandler<GetHostQuery, HostDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetHostQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<HostDto> Handle(GetHostQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Hosts
                .ProjectTo<HostDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (result == null) throw new NotFoundException("Hosts", request.Id);

            return result;
        }
    }
}
