//using FluentAssertions;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UserGroups.Application.Common.Exceptions;
//using UserGroups.Application.Common.Models;
//using UserGroups.Application.IntegrationTests.TestData;
//using UserGroups.Application.UserGroups.Meetings.Commands;

//namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
//{
//    using static TestDataManager;
//    using static Testing;
//    public class UpdateMeetingTests : TestBase
//    {
//        private UpdateMeetingCommand _command => new UpdateMeetingCommand
//        {
//            AllowRsvp = false,
//            EndTime = DateTime.Now,
//            Footer = "Updated Footer",
//            StartTime = DateTime.Now,
//            PublishStartTime = DateTime.Now,
//            Intro = "Updated Intro",
//            Title = "Updated Title",
//            IsDraft = true,
//            MaxRsvp = 999,
//            VimeoId = "UpdatedVimeo",
//            Tags = new List<string>()

//        };

//        [Test]
//        public async Task ShouldUpdateMeeting()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var createdAsTestUser = await CreateTestUser(new List<ApplicationRoles>() { ApplicationRoles.Admin });
//            var testMeeting = await CreateTestMeeting(createdAsTestUser.Id);
//            var request = _command;
//            request.Id = testMeeting.Id;
//            await SendAsync(request);

//            var updatedMeeting = await FindAsync<Domain.Entities.Meeting>(testMeeting.Id);

//            updatedMeeting.AllowRsvp.Should().Be(request.AllowRsvp);
//            updatedMeeting.StartTime.Should().Be(request.StartTime);
//            updatedMeeting.EndTime.Should().Be(request.EndTime);
//            updatedMeeting.PublishStartTime.Should().Be(request.PublishStartTime);
//            updatedMeeting.Footer.Should().Be(request.Footer);
//            updatedMeeting.Intro.Should().Be(request.Intro);
//            updatedMeeting.IsDraft.Should().Be(request.IsDraft);
//            updatedMeeting.Title.Should().Be(request.Title);
//            updatedMeeting.VimeoId.Should().Be(request.VimeoId);
//            updatedMeeting.HostMeetingBody.Should().Be(request.MeetingHostBody);
//            updatedMeeting.MeetingHostId.Should().Be(request.MeetingHostId);
//        }

//        [Test]
//        public void ShouldThrowIfMeetingNotFound()
//        {
//            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
//            var command = _command;
//            command.Id = 1;

//            FluentActions.Invoking(() =>
//                SendAsync(command)).Should().Throw<NotFoundException>();
//        }

//        [Test]
//        public void ShouldThrowIfUserIsNotMeetingAdmin()
//        {
//            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
//        }
//    }
//}
