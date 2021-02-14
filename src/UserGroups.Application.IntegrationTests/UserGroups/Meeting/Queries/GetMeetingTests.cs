using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Meetings.Queries;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Queries
{
    public class GetMeetingTests : TestBase
    {
        [Test]
        public async Task ShouldReturnTheMeeting()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testMeeting = await arrange.CreateTestMeeting();

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new GetMeetingQuery() { Id = testMeeting.Id });

            result.Id.Should().Be(testMeeting.Id);
            result.Title.Should().Be(testMeeting.Title);
            result.PublishStartTime.Should().Be(testMeeting.PublishStartTime);
            result.StartTime.Should().Be(testMeeting.StartTime);
            result.EndTime.Should().Be(testMeeting.EndTime);
            result.MaxRsvp.Should().Be(testMeeting.MaxRsvp);
            result.AllowRsvp.Should().Be(testMeeting.AllowRsvp);
            result.Intro.Should().Be(testMeeting.Intro);
            result.Footer.Should().Be(testMeeting.Footer);
            result.IsDraft.Should().Be(testMeeting.IsDraft);
            result.IsDeleted.Should().Be(testMeeting.IsDeleted);
            result.VimeoId.Should().Be(testMeeting.VimeoId);
            result.IsDeleted.Should().Be(testMeeting.IsDeleted);
            result.MeetingHostId.Should().Be(testMeeting.MeetingHostId);
            result.HostMeetingBody.Should().Be(testMeeting.HostMeetingBody);
        }

        [Test]
        public async Task ShouldReturnTheRsvpCount()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testMeeting = await arrange.CreateTestMeeting();
            var testUser1 = await arrange.CreateTestUser(new List<ApplicationRoles>() { ApplicationRoles.User });
            var testUser2 = await arrange.CreateTestUser(new List<ApplicationRoles>() { ApplicationRoles.User });
            await arrange.CreateTestMeetingRsvp(testUser1.Id, testMeeting.Id);
            await arrange.CreateTestMeetingRsvp(testUser2.Id, testMeeting.Id);

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var result = await act.SendAsync(new GetMeetingQuery() { Id = testMeeting.Id });

            result.Meta.RsvpCount.Should().Be(2);
        }

        [Test]
        public async Task ShouldThrowIfMeetingDoesNotExist()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            FluentActions.Invoking(() =>
                act.SendAsync(new GetMeetingQuery() { Id = 1 })).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldThrowIfUserIsNotHostAdmin()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });
            var command = new GetMeetingQuery { Id = 1 };
            FluentActions.Invoking(() => act.SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
