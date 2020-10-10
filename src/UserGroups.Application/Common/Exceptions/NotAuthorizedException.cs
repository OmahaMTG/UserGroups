using System;

namespace UserGroups.Application.Common.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException()
        {
        }

        public NotAuthorizedException(string message)
            : base(message)
        {
        }

        public NotAuthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotAuthorizedException(string userName, string request)
            : base($"unauthorized access by user \"{userName}\" using request \"{request}\"")
        {
        }
    }
}