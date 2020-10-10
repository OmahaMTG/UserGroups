using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Hosts.Queries;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Hosts.Queries
{
    using static TestDataManager;
    using static Testing;

    public class QueryHostTests : TestBase
    {


        [Test]
        public async Task ShouldReturnTheCreatedHost()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var dbHosts = new List<Host>();
            for (var i = 0; i < 10; i++)
                dbHosts.Add(
                    await CreateTestHost($"Test ${i} Blurb", $"Test ${i}  Name"));

            var result = await SendAsync(new QueryHostQuery
            {
                Skip = 5,
                Take = 10
            });

            result.TotalRecords.Should().Be(10);
            result.Records.Count().Should().Be(5);
            result.Skipped.Should().Be(5);

            result.Records.ElementAt(0).Name.Should().Be(dbHosts.ElementAt(5).Name);
            result.Records.ElementAt(0).ContactInfo.Should().Be(dbHosts.ElementAt(5).ContactInfo);

            result.Records.ElementAt(0).IsDeleted.Should().Be(dbHosts.ElementAt(5).IsDeleted);
            result.Records.ElementAt(0).Blurb.Should().Be(dbHosts.ElementAt(5).Blurb);

        }

        [Test]
        public async Task ShouldFilterHosts()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            for (var i = 0; i < 10; i++)
                await CreateTestHost($"Test ${i} Blurb", $"Test ${i}  Name");

            await CreateTestHost("Find Me ", "Test Name");
            await CreateTestHost("Test Blurb ", "Find Me");

            var result = await SendAsync(new QueryHostQuery
            {
                Skip = 0,
                Take = 10,
                Filter = "Find Me"
            });

            result.TotalRecords.Should().Be(2);
            result.Records.Count().Should().Be(2);
            result.Skipped.Should().Be(0);
        }

        [Test]
        public async Task ShouldIncludeDeletedWhenRequested()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            await CreateTestHost("Test Blurb", "Test Name", deleted: true);

            var result = await SendAsync(new QueryHostQuery
            {
                Skip = 0,
                Take = 10
            });

            result.TotalRecords.Should().Be(0);
            result.Records.Count().Should().Be(0);
        }

        [Test]
        public async Task ShouldExcludeDeletedByDefault()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            await CreateTestHost("Test Blurb", "Test Name", deleted: true);

            var result = await SendAsync(new QueryHostQuery
            {
                Skip = 0,
                Take = 10,
                IncludeDeleted = true
            });

            result.TotalRecords.Should().Be(1);
            result.Records.Count().Should().Be(1);
        }


        [Test]
        public void ShouldThrowIfUserIsNotHostAdmin()
        {
            var command = new QueryHostQuery
            {
                Skip = 0,
                Take = 10,
                IncludeDeleted = true
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
