using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Presenters.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Presenters.Commands
{

    public class CreatePresenterTests : TestBase
    {
        private CreatePresenterCommand _command => new CreatePresenterCommand()
        {
            ContactInfo = "Test Contact Info",
            IsDeleted = true,
            Name = "Test Name",
            Bio = "Test Bio"
        };


        [Test]
        public async Task ShouldCreatePresenter()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(_command);

            var assert = new Assert();
            var created = await assert.FindAsync<Presenter>(result);

            created.Name.Should().Be(_command.Name);
            created.Bio.Should().Be(_command.Bio);
            created.ContactInfo.Should().Be(_command.ContactInfo);
            created.IsDeleted.Should().Be(_command.IsDeleted);
            created.CreatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.CreatedByUserId.Should().Be(act.ActAsUser.Id);
            created.UpdatedByUserId.Should().Be(act.ActAsUser.Id);
        }

        [Test]
        public void ShouldThrowIfUserIsNotPresenterAdmin()
        {
            var act = new Act(new List<ApplicationRoles>() { ApplicationRoles.User });
            FluentActions.Invoking(() => act.SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
