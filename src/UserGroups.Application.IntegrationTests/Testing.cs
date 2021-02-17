using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;
using UserGroups.Infrastructure;
using UserGroups.Infrastructure.Persistence;

namespace UserGroups.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        private static readonly bool _isLoggedIn = true;
        private static string _userId = "UserId";
        private static string _userName = "UserName";
        private static IEnumerable<ApplicationRoles> _roles = new List<ApplicationRoles>();


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            _configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });
            services.AddInfrastructure(_configuration);
            services.AddApplication(_configuration);


            services.ReplaceService<IUserContext>(provider =>
                Mock.Of<IUserContext>(s => s.IsLoggedIn() == _isLoggedIn
                                           && s.UserId == _userId
                                           && s.UserName == _userName
                                           && s.Roles == _roles
                )
            );

            services.ReplaceService<ITimeUtility>(provider =>
                Mock.Of<ITimeUtility>(s => s.GetCurrentSystemTime == DateTime.Now
                )
            );


            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            EnsureDatabase();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory", "AspNetRoles" }
            };
        }

        public static void SetCurrentUser(string userId, IEnumerable<ApplicationRoles> roles)
        {
            _userId = userId;
            _roles = roles;
        }


        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

            _roles = new List<ApplicationRoles>();
            _userId = null;
            _userName = null;
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
