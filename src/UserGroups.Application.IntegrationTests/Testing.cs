using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private static readonly string _userId = "UserId";
        private static readonly string _userName = "UserName";
        private static List<ApplicationRoles> _roles = new List<ApplicationRoles> { ApplicationRoles.Admin };


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            _configuration = builder.Build();

            var services = new ServiceCollection();
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
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
        }


        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //context.Database.Migrate();
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
            //     _currentUserId = null;

            _roles = new List<ApplicationRoles>();
        }

        public static void SetRoles(List<ApplicationRoles> roles)
        {
            _roles = roles;
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

        public static async Task<IEnumerable<TEntity>> FindAllAsync<TEntity>()
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.Set<TEntity>().ToListAsync();
        }

        public static async Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(string include)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.Set<TEntity>().Include(include).ToListAsync();
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
