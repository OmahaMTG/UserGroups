using System.Collections.Generic;
using System.Linq;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.Common.Interfaces
{
    public interface IUserContext
    {
        string UserId { get; }
        string UserName { get; }
        IEnumerable<ApplicationRoles> Roles { get; }
        bool IsLoggedIn();

        bool UserHasRole(ApplicationRoles role)
        {
            return Roles.Contains(role);
        }
    }
}
