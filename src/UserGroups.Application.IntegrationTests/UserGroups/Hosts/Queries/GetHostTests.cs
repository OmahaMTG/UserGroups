using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Hosts.Queries;

namespace UserGroups.Application.IntegrationTests.UserGroups.Hosts.Queries
{
    using static TestDataManager;
    using static Testing;

    public class GetHostTests : TestBase
    {


        [Test]
        public async Task ShouldReturnTheHost()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testHost = await CreateTestHost();

            var result = await SendAsync(new GetHostQuery { Id = testHost.Id });

            result.Name.Should().Be(testHost.Name);
            result.Blurb.Should().Be(testHost.Blurb);
            result.ContactInfo.Should().Be(testHost.ContactInfo);
            result.IsDeleted.Should().Be(testHost.IsDeleted);

        }

        [Test]
        public void ShouldThrowIfHostDoesNotExist()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            FluentActions.Invoking(() =>
                SendAsync(new GetHostQuery { Id = 1 })).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotHostAdmin()
        {
            var command = new GetHostQuery { Id = 1 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
