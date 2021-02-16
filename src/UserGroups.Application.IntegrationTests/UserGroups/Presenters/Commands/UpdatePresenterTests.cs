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
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testPresenter = await arrange.CreateTestPresenter();

            var act = new Act();
            var user = await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = testPresenter.Id;
            await act.SendAsync(command);

            var assert = new Assert();
            var updatedPresenter = await assert.FindAsync<Presenter>(testPresenter.Id);

            updatedPresenter.Name.Should().Be(command.Name);
            updatedPresenter.ContactInfo.Should().Be(command.ContactInfo);
            updatedPresenter.IsDeleted.Should().Be(command.IsDeleted);
            updatedPresenter.UpdatedDate.Should().BeCloseTo(DateTime.Now, 10000);
            updatedPresenter.UpdatedByUserId.Should().Be(user.Id);
        }

        [Test]
        public async Task ShouldThrowIfPresenterNotFound()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = 1;

            FluentActions.Invoking(() => act.SendAsync(command)).Should().Throw<NotFoundException>();
        }



        [Test]
        public async Task ShouldThrowIfUserIsNotPresenterAdmin()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });
            FluentActions.Invoking(() => act.SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
