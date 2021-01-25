using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.IntegrationTests.TestData;
using UserGroups.Application.UserGroups.Sponsors.Queries;
namespace UserGroups.Application.IntegrationTests.UserGroups.Sponsors.Queries
{
    using static TestDataManager;
    using static Testing;

    public class GetSponsorTests : TestBase
    {

        [Test]
        public async Task ShouldReturnTheSponsor()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testSponsor = await CreateTestSponsor();

            var result = await SendAsync(new GetSponsorQuery { Id = testSponsor.Id });

            result.Name.Should().Be(testSponsor.Name);
            result.Blurb.Should().Be(testSponsor.Blurb);
            result.ContactInfo.Should().Be(testSponsor.ContactInfo);
            result.IncludeInBannerRotation.Should().Be(testSponsor.IncludeInBannerRotation);
            result.IsDeleted.Should().Be(testSponsor.IsDeleted);
            result.ShortBlurb.Should().Be(testSponsor.ShortBlurb);
            result.SponsorUrl.Should().Be(testSponsor.SponsorUrl);
        }

        [Test]
        public void ShouldThrowIfSponsorDoesNotExist()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            FluentActions.Invoking(() =>
                SendAsync(new GetSponsorQuery { Id = 1 })).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotSponsorAdmin()
        {
            var command = new GetSponsorQuery { Id = 1 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
