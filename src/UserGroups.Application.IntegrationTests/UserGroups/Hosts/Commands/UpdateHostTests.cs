using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Hosts.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Hosts.Commands
{
    public class UpdateHostTests : TestBase
    {
        private UpdateHostCommand _command => new UpdateHostCommand
        {
            Blurb = "Updated Blurb",
            ContactInfo = "Updated Contact Info",
            IsDeleted = false,
            Name = "Updated Name",
        };


        [Test]
        public async Task ShouldUpdateHost()
        {
            var arrange = new Arrange();
            var testHost = await arrange.CreateTestHost();

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = testHost.Id;
            await act.SendAsync(command);

            var assert = new Assert();
            var updatedHost = await assert.FindAsync<Host>(testHost.Id);

            updatedHost.Name.Should().Be(command.Name);
            updatedHost.Blurb.Should().Be(command.Blurb);
            updatedHost.ContactInfo.Should().Be(command.ContactInfo);
            updatedHost.IsDeleted.Should().Be(command.IsDeleted);
            updatedHost.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            updatedHost.UpdatedByUserId.Should().Be(act.ActAsUser.Id);
        }

        [Test]
        public void ShouldThrowIfHostNotFound()
        {

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = 1;

            FluentActions.Invoking(() =>
                act.SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotHostAdmin()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.User });
            FluentActions.Invoking(() => act.SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
