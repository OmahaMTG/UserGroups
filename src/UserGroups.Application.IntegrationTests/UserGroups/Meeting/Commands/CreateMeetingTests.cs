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

    public class CreateMeetingTests : TestBase
    {

        private CreateMeetingCommand Command() => new CreateMeetingCommand()
        {
            Title = "Test Title",
            AllowRsvp = false,
            EndTime = DateTime.Now,
            Footer = "Test Footer",
            Intro = "test intro",
            IsDraft = true,
            MaxRsvp = 10,
            PublishStartTime = DateTime.Now,
            StartTime = DateTime.Now,
            VimeoId = "1234",

        };


        [Test]
        public async Task ShouldCreateTheMeeting()
        {

            var act = new Act(new List<ApplicationRoles>() { ApplicationRoles.Admin });
            var command = Command();
            var createdId = await act.SendAsync(command);

            var assert = new Assert();
            var dbMeeting = await assert.FindAsync<Domain.Entities.Meeting>(createdId);

            dbMeeting.Title.Should().Be(command.Title);
            dbMeeting.AllowRsvp.Should().Be(command.AllowRsvp);
            dbMeeting.EndTime.Should().Be(command.EndTime);
            dbMeeting.Footer.Should().Be(command.Footer);
            dbMeeting.Intro.Should().Be(command.Intro);
            dbMeeting.IsDraft.Should().Be(command.IsDraft);
            dbMeeting.MaxRsvp.Should().Be(command.MaxRsvp);
            dbMeeting.PublishStartTime.Should().Be(command.PublishStartTime);
            dbMeeting.StartTime.Should().Be(command.StartTime);
            dbMeeting.VimeoId.Should().Be(command.VimeoId);
            dbMeeting.CreatedByUserId.Should().Be(act.ActAsUser.Id);
            dbMeeting.UpdatedByUserId.Should().Be(act.ActAsUser.Id);
            dbMeeting.CreatedDate.Should().BeCloseTo(DateTime.Now, 1000);
            dbMeeting.UpdatedDate.Should().BeCloseTo(DateTime.Now, 1000);
            dbMeeting.HostMeetingBody.Should().Be(command.MeetingHostBody);
            dbMeeting.MeetingHostId.Should().Be(command.MeetingHostId);

        }


        [Test]
        public void ShouldThrowIfUserIsNotAdmin()
        {
            var act = new Act(new List<ApplicationRoles> { ApplicationRoles.User });

            FluentActions.Invoking(() => act.SendAsync(Command())).Should().Throw<NotAuthorizedException>();
        }


    }
}
