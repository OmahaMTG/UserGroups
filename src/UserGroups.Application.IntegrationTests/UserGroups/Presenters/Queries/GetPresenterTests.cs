using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Presenters.Queries;

namespace UserGroups.Application.IntegrationTests.UserGroups.Presenters.Queries
{


    public class GetPresenterTests : TestBase
    {

        [Test]
        public async Task ShouldReturnThePresenter()
        {
            var arrange = new Arrange();
            var testPresenter = await arrange.CreateTestPresenter();

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new GetPresenterQuery { Id = testPresenter.Id });

            result.Name.Should().Be(testPresenter.Name);
            result.ContactInfo.Should().Be(testPresenter.ContactInfo);
            result.IsDeleted.Should().Be(testPresenter.IsDeleted);

        }

        [Test]
        public void ShouldThrowIfPresenterDoesNotExist()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            FluentActions.Invoking(() => act.SendAsync(new GetPresenterQuery { Id = 1 })).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotPresenterAdmin()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.User });
            var command = new GetPresenterQuery { Id = 1 };
            FluentActions.Invoking(() => act.SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
