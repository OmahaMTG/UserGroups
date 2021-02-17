using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Presenters.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Presenters.Commands
{
    public class DeletePresenterTests : TestBase
    {
        [Test]
        public async Task HardDeleteShouldDeletePresenter()
        {
            var arrange = new Arrange();
            var testPresenter = await arrange.CreateTestPresenter();

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            await act.SendAsync(new DeletePresenterCommand
            {
                HardDelete = true,
                Id = testPresenter.Id
            });

            var assert = new Assert();
            var dbPresenter = await assert.FindAsync<Presenter>(testPresenter.Id);

            dbPresenter.Should().BeNull();
        }

        [Test]
        public async Task SoftDeleteShouldFlagPresenterAsDeleted()
        {
            var arrange = new Arrange();
            var testPresenter = await arrange.CreateTestPresenter();

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            await act.SendAsync(new DeletePresenterCommand
            {
                HardDelete = false,
                Id = testPresenter.Id
            });

            var assert = new Assert();
            var dbPresenter = await assert.FindAsync<Presenter>(testPresenter.Id);

            dbPresenter.Should().NotBeNull();
            dbPresenter.IsDeleted.Should().BeTrue();
        }


        [Test]
        public void ShouldThrowIfPresenterIdDoesNotExist()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var deleteCommand = new DeletePresenterCommand
            {
                HardDelete = false,
                Id = 1
            };

            FluentActions.Invoking(() => act.SendAsync(deleteCommand)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotPresenterAdmin()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.User });
            var deleteCommand = new DeletePresenterCommand
            {
                HardDelete = false,
                Id = 1
            };

            FluentActions.Invoking(() => act.SendAsync(deleteCommand)).Should().Throw<NotAuthorizedException>();
        }
    }
}
