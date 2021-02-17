using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Interfaces;

namespace UserGroups.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUserContext _userContext;

        public AuthorizationBehaviour(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var authAttribute = Attribute.GetCustomAttributes(typeof(TRequest), typeof(AuthorizationAttribute))
                .Select(s => (AuthorizationAttribute)s)
                .FirstOrDefault(); // Reflection.  

            if (authAttribute == null) return await next();

            var matchingRoles = authAttribute.Roles.Intersect(_userContext.Roles);

            if (matchingRoles.Any()) return await next();
            throw new NotAuthorizedException(_userContext.UserName, request.GetType().FullName);
        }
    }
}
