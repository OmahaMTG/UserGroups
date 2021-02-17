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

    public class DeleteHostTests : TestBase
    {
        [Test]
        public async Task HardDeleteShouldDeleteHost()
        {
            var arrange = new Arrange();
            var testHost = await arrange.CreateTestHost();

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            await act.SendAsync(new DeleteHostCommand()
            {
                HardDelete = true,
                Id = testHost.Id
            });

            var assert = new Assert();
            var dbHost = await assert.FindAsync<Host>(testHost.Id);
            dbHost.Should().BeNull();
        }

        [Test]
        public async Task SoftDeleteShouldFlagHostAsDeleted()
        {
            var arrange = new Arrange();
            var testHost = await arrange.CreateTestHost();

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            await act.SendAsync(new DeleteHostCommand()
            {
                HardDelete = false,
                Id = testHost.Id
            });

            var assert = new Assert();
            var dbHost = await assert.FindAsync<Host>(testHost.Id);
            dbHost.Should().NotBeNull();
            dbHost.IsDeleted.Should().BeTrue();
        }


        [Test]
        public void ShouldThrowIfHostIdDoesNotExist()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });

            var deleteCommand = new DeleteHostCommand
            {
                HardDelete = false,
                Id = 1
            };

            FluentActions.Invoking(() => act.SendAsync(deleteCommand)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotHostAdmin()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.User });
            var deleteCommand = new DeleteHostCommand
            {
                HardDelete = false,
                Id = 1
            };

            FluentActions.Invoking(() => act.SendAsync(deleteCommand)).Should().Throw<NotAuthorizedException>();
        }
    }
}
