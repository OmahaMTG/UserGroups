using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests
{
    using static Testing;
    public class Act
    {
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return await Testing.SendAsync<TResponse>(request);
        }

        public async Task<OmahaMtgUser> SetActUser(IList<ApplicationRoles> roles)
        {
            var user = await AddAsync<OmahaMtgUser>(
                new OmahaMtgUser()
                {
                    FirstName = "Act",
                    LastName = "User"

                });

            foreach (var role in roles)
            {
                await AddAsync<IdentityUserRole<string>>(new IdentityUserRole<string>()
                {
                    RoleId = role.ToString(),
                    UserId = user.Id
                });
            }

            SetCurrentUser(user.Id, roles);

            return user;
        }

    }
}
