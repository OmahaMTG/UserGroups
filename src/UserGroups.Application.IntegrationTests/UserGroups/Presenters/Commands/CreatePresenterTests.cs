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
    using static Testing;
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
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await SendAsync(_command);

            var created = await FindAsync<Presenter>(result);

            created.Name.Should().Be(_command.Name);
            created.Bio.Should().Be(_command.Bio);
            created.ContactInfo.Should().Be(_command.ContactInfo);
            created.IsDeleted.Should().Be(_command.IsDeleted);
            created.CreatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            created.CreatedByUser.Should().Be("UserId");
            created.UpdatedByUser.Should().Be("UserId");
        }

        [Test]
        public void ShouldThrowIfUserIsNotPresenterAdmin()
        {
            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
