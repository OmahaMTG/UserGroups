using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UserGroups.Application.IntegrationTests
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection ReplaceService<T>(this ServiceCollection serviceCollection,
            Func<IServiceProvider, object> factory)
        {
            var currentUserServiceDescriptor = serviceCollection.FirstOrDefault(d =>
                d.ServiceType == typeof(T));

            serviceCollection.Remove(currentUserServiceDescriptor);


            serviceCollection.Add(new ServiceDescriptor(typeof(T), factory,
                currentUserServiceDescriptor?.Lifetime ?? ServiceLifetime.Transient));

            return serviceCollection;
        }
    }
}