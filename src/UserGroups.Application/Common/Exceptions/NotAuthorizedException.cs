using System;

namespace UserGroups.Application.Common.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string userName, string request)
            : base($"unauthorized access by user \"{userName}\" using request \"{request}\"")
        {
        }
    }
}
