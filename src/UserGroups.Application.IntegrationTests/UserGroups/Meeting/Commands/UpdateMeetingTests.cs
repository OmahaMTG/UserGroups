using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Meetings.Commands;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
{

    public class UpdateMeetingTests : TestBase
    {
        private UpdateMeetingCommand _command => new UpdateMeetingCommand
        {
            AllowRsvp = false,
            EndTime = DateTime.Now,
            Footer = "Updated Footer",
            StartTime = DateTime.Now,
            PublishStartTime = DateTime.Now,
            Intro = "Updated Intro",
            Title = "Updated Title",
            IsDraft = true,
            MaxRsvp = 999,
            VimeoId = "UpdatedVimeo",
            Tags = new List<string>()

        };

        [Test]
        public async Task ShouldUpdateMeeting()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();
            var testMeeting = await arrange.CreateTestMeeting();

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var request = _command;
            request.Id = testMeeting.Id;
            await act.SendAsync(request);

            var assert = new Assert();
            var updatedMeeting = await assert.FindAsync<Domain.Entities.Meeting>(testMeeting.Id);
            updatedMeeting.AllowRsvp.Should().Be(request.AllowRsvp);
            updatedMeeting.StartTime.Should().Be(request.StartTime);
            updatedMeeting.EndTime.Should().Be(request.EndTime);
            updatedMeeting.PublishStartTime.Should().Be(request.PublishStartTime);
            updatedMeeting.Footer.Should().Be(request.Footer);
            updatedMeeting.Intro.Should().Be(request.Intro);
            updatedMeeting.IsDraft.Should().Be(request.IsDraft);
            updatedMeeting.Title.Should().Be(request.Title);
            updatedMeeting.VimeoId.Should().Be(request.VimeoId);
            updatedMeeting.HostMeetingBody.Should().Be(request.MeetingHostBody);
            updatedMeeting.MeetingHostId.Should().Be(request.MeetingHostId);
        }

        [Test]
        public async Task ShouldThrowIfMeetingNotFound()
        {
            var arrange = new Arrange();
            await arrange.SetArrangeUser();

            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var request = _command;
            request.Id = 1;

            FluentActions.Invoking(() =>
                act.SendAsync(request)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldThrowIfUserIsNotMeetingAdmin()
        {
            var act = new Act();
            await act.SetActUser(new List<ApplicationRoles> { ApplicationRoles.User });

            FluentActions.Invoking(() => act.SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
