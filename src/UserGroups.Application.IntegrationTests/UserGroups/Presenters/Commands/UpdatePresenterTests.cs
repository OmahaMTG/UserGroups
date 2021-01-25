using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.IntegrationTests.TestData;
using UserGroups.Application.UserGroups.Presenters.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Presenters.Commands
{
    using static TestDataManager;
    using static Testing;

    public class UpdatePresenterTests : TestBase
    {
        private UpdatePresenterCommand _command => new UpdatePresenterCommand
        {
            ContactInfo = "Updated Contact Info",
            IsDeleted = false,
            Name = "Updated Name",
        };



        [Test]
        public async Task ShouldUpdatePresenter()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testPresenter = await CreateTestPresenter();
            var command = _command;
            command.Id = testPresenter.Id;
            await SendAsync(command);

            var updatedPresenter = await FindAsync<Presenter>(testPresenter.Id);

            updatedPresenter.Name.Should().Be(command.Name);
            updatedPresenter.ContactInfo.Should().Be(command.ContactInfo);
            updatedPresenter.IsDeleted.Should().Be(command.IsDeleted);

            updatedPresenter.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            updatedPresenter.UpdatedByUser.Should().Be("UserId");
        }

        [Test]
        public void ShouldThrowIfPresenterNotFound()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = 1;

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }



        [Test]
        public void ShouldThrowIfUserIsNotPresenterAdmin()
        {
            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
