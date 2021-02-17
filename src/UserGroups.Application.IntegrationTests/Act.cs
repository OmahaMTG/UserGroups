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
        private readonly IList<ApplicationRoles> _actAsUserRoles;
        public OmahaMtgUser ActAsUser { get; private set; }

        public Act(IList<ApplicationRoles> actAsUserRoles)
        {
            _actAsUserRoles = actAsUserRoles;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            await EnsureActAsUser();
            return await Testing.SendAsync(request);
        }

        public async Task EnsureActAsUser()
        {
            if (ActAsUser == null)
            {
                var user = await AddAsync(
                                new OmahaMtgUser()
                                {
                                    FirstName = "Act",
                                    LastName = "User"

                                });

                foreach (var role in _actAsUserRoles)
                {
                    await AddAsync(new IdentityUserRole<string>()
                    {
                        RoleId = role.ToString(),
                        UserId = user.Id
                    });
                }

                ActAsUser = user;
            }


            SetCurrentUser(ActAsUser.Id, _actAsUserRoles);


        }

    }
}
