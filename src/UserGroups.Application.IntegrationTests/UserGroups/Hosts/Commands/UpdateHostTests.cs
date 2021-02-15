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
            await arrange.SetArrangeUser();
            var testHost = await arrange.CreateTestHost();

            var act = new Act();
            var command = _command;
            command.Id = testHost.Id;
            var user = await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            await act.SendAsync(command);

            var assert = new Assert();
            var updatedHost = await assert.FindAsync<Host>(testHost.Id);

            updatedHost.Name.Should().Be(command.Name);
            updatedHost.Blurb.Should().Be(command.Blurb);
            updatedHost.ContactInfo.Should().Be(command.ContactInfo);
            updatedHost.IsDeleted.Should().Be(command.IsDeleted);
            updatedHost.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            updatedHost.UpdatedByUserId.Should().Be(user.Id);
        }

        [Test]
        public async Task ShouldThrowIfHostNotFound()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testMeeting = await arrange.CreateTestHost();

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = 1;

            FluentActions.Invoking(() =>
                act.SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldThrowIfUserIsNotHostAdmin()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });
            FluentActions.Invoking(() => act.SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
