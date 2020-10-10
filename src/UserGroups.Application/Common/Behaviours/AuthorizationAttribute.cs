using System;
using UserGroups.Application.Common.Models;

namespace UserGroups.Application.Common.Behaviours
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizationAttribute : Attribute
    {
        public AuthorizationAttribute(params ApplicationRoles[] roles)
        {
            Roles = roles;
        }

        public ApplicationRoles[] Roles { get; }
    }
}
