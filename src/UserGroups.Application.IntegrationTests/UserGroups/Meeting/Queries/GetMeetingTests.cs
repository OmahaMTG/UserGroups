using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.IntegrationTests.TestData;
using UserGroups.Application.UserGroups.Meetings.Queries;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Queries
{
    using static TestDataManager;
    using static Testing;

    public class GetMeetingTests : TestBase
    {


        [Test]
        public async Task ShouldReturnTheMeeting()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testMeeting = await CreateTestMeeting();

            var result = await SendAsync(new GetMeetingQuery() { Id = testMeeting.Id });

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

        //test meeting host
        //test RSVP counts

        [Test]
        public async Task ShouldReturnTheRsvpCount()
        {
            var testMeeting = await CreateTestMeeting();
            var testUser1 = await CreateTestUser();
            var testUser2 = await CreateTestUser();
            await AddMeetingRsvp(testUser1.Id, testMeeting.Id);
            await AddMeetingRsvp(testUser2.Id, testMeeting.Id);

            var result = await SendAsync(new GetMeetingQuery() { Id = testMeeting.Id });

            result.Meta.RsvpCount.Should().Be(2);
        }




        [Test]
        public void ShouldThrowIfMeetingDoesNotExist()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            FluentActions.Invoking(() =>
                SendAsync(new GetMeetingQuery() { Id = 1 })).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotHostAdmin()
        {
            var command = new GetMeetingQuery { Id = 1 };

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
