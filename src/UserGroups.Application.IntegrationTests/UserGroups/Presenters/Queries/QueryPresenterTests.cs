using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Presenters.Queries;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Presenters.Queries
{
    public class QueryPresenterTests : TestBase
    {

        [Test]
        public async Task ShouldReturnTheCreatedPresenter()
        {
            var arrange = new Arrange();
            var dbPresenters = new List<Presenter>();
            for (var i = 0; i < 10; i++)
                dbPresenters.Add(
                    await arrange.CreateTestPresenter($"Test ${i} Blurb"));

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryPresenterQuery
            {
                Skip = 5,
                Take = 10
            });

            result.TotalRecords.Should().Be(10);
            result.Records.Count().Should().Be(5);
            result.Skipped.Should().Be(5);
            result.Records.ElementAt(0).Name.Should().Be(dbPresenters.ElementAt(5).Name);
            result.Records.ElementAt(0).ContactInfo.Should().Be(dbPresenters.ElementAt(5).ContactInfo);
            result.Records.ElementAt(0).IsDeleted.Should().Be(dbPresenters.ElementAt(5).IsDeleted);

        }

        [Test]
        public async Task ShouldFilterPresenters()
        {
            var arrange = new Arrange();
            for (var i = 0; i < 10; i++)
                await arrange.CreateTestPresenter($"Test ${i} Blurb", $"Test ${i} Short Bio", isDeleted: false);

            await arrange.CreateTestPresenter(name: "Find Me ", bio: "Test  Short Blurb", isDeleted: false);
            await arrange.CreateTestPresenter(name: "Test Blurb", bio: "Find Me", isDeleted: false);

            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryPresenterQuery
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
            await arrange.CreateTestPresenter("Test Blurb", "Test Short Blurb", isDeleted: true);

            var act = new Act(new List<ApplicationRoles>() { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryPresenterQuery
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
            await arrange.CreateTestPresenter("Test Blurb", "Test Short Blurb", isDeleted: true);

            var act = new Act(new List<ApplicationRoles>() { ApplicationRoles.Admin });
            var result = await act.SendAsync(new QueryPresenterQuery
            {
                Skip = 0,
                Take = 10,
                IncludeDeleted = true
            });

            result.TotalRecords.Should().Be(1);
            result.Records.Count().Should().Be(1);
        }


        [Test]
        public void ShouldThrowIfUserIsNotPresenterAdmin()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.User });
            var command = new QueryPresenterQuery
            {
                Skip = 0,
                Take = 10,
                IncludeDeleted = true
            };

            FluentActions.Invoking(() => act.SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
