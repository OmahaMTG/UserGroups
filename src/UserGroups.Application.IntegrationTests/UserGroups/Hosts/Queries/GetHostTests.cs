using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Hosts.Queries;

namespace UserGroups.Application.IntegrationTests.UserGroups.Hosts.Queries
{

    public class GetHostTests : TestBase
    {
        [Test]
        public async Task ShouldReturnTheHost()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testHost = await arrange.CreateTestHost();

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new GetHostQuery { Id = testHost.Id });

            result.Name.Should().Be(testHost.Name);
            result.Blurb.Should().Be(testHost.Blurb);
            result.ContactInfo.Should().Be(testHost.ContactInfo);
            result.IsDeleted.Should().Be(testHost.IsDeleted);

        }

        [Test]
        public async Task ShouldThrowIfHostDoesNotExist()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            FluentActions.Invoking(() =>
                act.SendAsync(new GetHostQuery { Id = 1 })).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldThrowIfUserIsNotHostAdmin()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });

            var command = new GetHostQuery { Id = 1 };
            FluentActions.Invoking(() => act.SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
