using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Hosts.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Hosts.Commands
{
    using static TestDataManager;
    using static Testing;
    public class DeleteHostTests : TestBase
    {
        [Test]
        public async Task HardDeleteShouldDeleteHost()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testHost = await CreateTestHost();

            await SendAsync(new DeleteHostCommand()
            {
                HardDelete = true,
                Id = testHost.Id
            });

            var dbHost = await FindAsync<Host>(testHost.Id);

            dbHost.Should().BeNull();
        }

        [Test]
        public async Task SoftDeleteShouldFlagHostAsDeleted()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

            var testHost = await CreateTestHost();

            await SendAsync(new DeleteHostCommand()
            {
                HardDelete = false,
                Id = testHost.Id
            });

            var dbHost = await FindAsync<Host>(testHost.Id);

            dbHost.Should().NotBeNull();

            dbHost.IsDeleted.Should().BeTrue();
        }


        [Test]
        public void ShouldThrowIfHostIdDoesNotExist()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var deleteCommand = new DeleteHostCommand
            {
                HardDelete = false,
                Id = 1
            };

            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotHostAdmin()
        {
            var deleteCommand = new DeleteHostCommand
            {
                HardDelete = false,
                Id = 1
            };

            FluentActions.Invoking(() => SendAsync(deleteCommand)).Should().Throw<NotAuthorizedException>();
        }
    }
}
