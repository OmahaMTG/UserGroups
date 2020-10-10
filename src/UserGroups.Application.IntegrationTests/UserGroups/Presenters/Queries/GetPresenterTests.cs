using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Presenters.Queries;

namespace UserGroups.Application.IntegrationTests.UserGroups.Presenters.Queries
{
    using static TestDataManager;
    using static Testing;

    public class GetPresenterTests : TestBase
    {

        [Test]
        public async Task ShouldReturnThePresenter()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testPresenter = await CreateTestPresenter();

            var result = await SendAsync(new GetPresenterQuery { Id = testPresenter.Id });

            result.Name.Should().Be(testPresenter.Name);
            result.ContactInfo.Should().Be(testPresenter.ContactInfo);
            result.IsDeleted.Should().Be(testPresenter.IsDeleted);

        }

        [Test]
        public void ShouldThrowIfPresenterDoesNotExist()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            FluentActions.Invoking(() =>
                SendAsync(new GetPresenterQuery { Id = 1 })).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotPresenterAdmin()
        {
            var command = new GetPresenterQuery { Id = 1 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
