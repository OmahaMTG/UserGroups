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

    public class QueryHostTests : TestBase
    {


        [Test]
        public async Task ShouldReturnTheCreatedHost()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();

            var dbHosts = new List<Host>();
            for (var i = 0; i < 10; i++)
                dbHosts.Add(
                    await arrange.CreateTestHost($"Test ${i} Blurb", $"Test ${i}  Name"));

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryHostQuery
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
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            for (var i = 0; i < 10; i++)
                await arrange.CreateTestHost($"Test ${i} Blurb", $"Test ${i}  Name");

            await arrange.CreateTestHost("Find Me ", "Test Name");
            await arrange.CreateTestHost("Test Blurb ", "Find Me");

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryHostQuery
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
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            await arrange.CreateTestHost("Test Blurb", "Test Name", isDeleted: true);

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryHostQuery
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
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            await arrange.CreateTestHost("Test Blurb", "Test Name", isDeleted: true);

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryHostQuery
            {
                Skip = 0,
                Take = 10,
                IncludeDeleted = true
            });

            result.TotalRecords.Should().Be(1);
            result.Records.Count().Should().Be(1);
        }


        [Test]
        public async Task ShouldThrowIfUserIsNotHostAdmin()
        {
            var command = new QueryHostQuery
            {
                Skip = 0,
                Take = 10,
                IncludeDeleted = true
            };

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });
            FluentActions.Invoking(() => act.SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
