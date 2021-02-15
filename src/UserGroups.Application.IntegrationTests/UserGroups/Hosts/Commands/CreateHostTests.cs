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
    public class CreateHostTests : TestBase
    {
        private CreateHostCommand _command => new CreateHostCommand
        {
            Blurb = "Test Blurb",
            ContactInfo = "Test Contact Info",
            IsDeleted = true,
            Name = "Test Name"
        };


        [Test]
        public async Task ShouldCreateHost()
        {
            var act = new Act();
            var user = await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(_command);

            var assert = new Assert();
            var created = await assert.FindAsync<Host>(result);

            created.Name.Should().Be(_command.Name);
            created.Blurb.Should().Be(_command.Blurb);
            created.ContactInfo.Should().Be(_command.ContactInfo);
            created.IsDeleted.Should().Be(_command.IsDeleted);
            created.CreatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.CreatedByUserId.Should().Be(user.Id);
            created.UpdatedByUserId.Should().Be(user.Id);
        }


        [Test]
        public async Task ShouldThrowIfUserIsNotSponsorAdmin()
        {
            var act = new Act();
            var user = await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });
            FluentActions.Invoking(() => act.SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
