//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using FluentAssertions;
//using NUnit.Framework;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.UserGroups.Sponsors.Queries;
//using UserGroups.Domain.Entities;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Sponsors.Queries
//{
//    using static Testing;

//    public class QuerySponsorTests : TestBase
//    {
//        private async Task<Sponsor> CreateTestSponsor(string blurb, string name, string shortBlurb,
//            bool deleted = false)
//        {
//            return await AddAsync(new Sponsor
//            {
//                Blurb = blurb,
//                ContactInfo = "Test Contact Info",
//                IncludeInBannerRotation = true,
//                IsDeleted = deleted,
//                Name = name,
//                ShortBlurb = shortBlurb,
//                SponsorUrl = "http://test.url"
//            });
//        }

//        [Test]
//        public async Task ShouldReturnTheCreatedSponsor()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var dbSponsors = new List<Sponsor>();
//            for (var i = 0; i < 10; i++)
//                dbSponsors.Add(
//                    await CreateTestSponsor($"Test ${i} Blurb", $"Test ${i}  Name", $"Test ${i} Short Blurb"));

//            var result = await SendAsync(new QuerySponsorQuery
//            {
//                Skip = 5,
//                Take = 10
//            });

//            result.TotalRecords.Should().Be(10);
//            result.Records.Count().Should().Be(5);
//            result.Skipped.Should().Be(5);

//            result.Records.ElementAt(0).Name.Should().Be(dbSponsors.ElementAt(5).Name);
//            result.Records.ElementAt(0).ContactInfo.Should().Be(dbSponsors.ElementAt(5).ContactInfo);
//            result.Records.ElementAt(0).IncludeInBannerRotation.Should()
//                .Be(dbSponsors.ElementAt(5).IncludeInBannerRotation);
//            result.Records.ElementAt(0).IsDeleted.Should().Be(dbSponsors.ElementAt(5).IsDeleted);
//            result.Records.ElementAt(0).Blurb.Should().Be(dbSponsors.ElementAt(5).Blurb);
//            result.Records.ElementAt(0).ShortBlurb.Should().Be(dbSponsors.ElementAt(5).ShortBlurb);
//            result.Records.ElementAt(0).SponsorUrl.Should().Be(dbSponsors.ElementAt(5).SponsorUrl);
//        }

//        [Test]
//        public async Task ShouldFilterSponsors()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            for (var i = 0; i < 10; i++)
//                await CreateTestSponsor($"Test ${i} Blurb", $"Test ${i}  Name", $"Test ${i} Short Blurb");

//            await CreateTestSponsor("Find Me ", "Test Name", "Test  Short Blurb");
//            await CreateTestSponsor("Test Blurb ", "Find Me", "Test  Short Blurb");
//            await CreateTestSponsor("Test Blurb", "Test Name", "Find Me");

//            var result = await SendAsync(new QuerySponsorQuery
//            {
//                Skip = 0,
//                Take = 10,
//                Filter = "Find Me"
//            });

//            result.TotalRecords.Should().Be(3);
//            result.Records.Count().Should().Be(3);
//            result.Skipped.Should().Be(0);
//        }

//        [Test]
//        public async Task ShouldIncludeDeletedWhenRequested()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            await CreateTestSponsor("Test Blurb", "Test Name", "Test Short Blurb", true);

//            var result = await SendAsync(new QuerySponsorQuery
//            {
//                Skip = 0,
//                Take = 10
//            });

//            result.TotalRecords.Should().Be(0);
//            result.Records.Count().Should().Be(0);
//        }

//        [Test]
//        public async Task ShouldExcludeDeletedByDefault()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            await CreateTestSponsor("Test Blurb", "Test Name", "Test Short Blurb", true);

//            var result = await SendAsync(new QuerySponsorQuery
//            {
//                Skip = 0,
//                Take = 10,
//                IncludeDeleted = true
//            });

//            result.TotalRecords.Should().Be(1);
//            result.Records.Count().Should().Be(1);
//        }


//        [Test]
//        public void ShouldThrowIfUserIsNotSponsorAdmin()
//        {
//            var command = new QuerySponsorQuery
//            {
//                Skip = 0,
//                Take = 10,
//                IncludeDeleted = true
//            };

//            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotAuthorizedException>();
//        }
//    }
//}
