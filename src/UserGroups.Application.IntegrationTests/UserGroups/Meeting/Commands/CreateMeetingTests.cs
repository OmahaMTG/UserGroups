using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Meetings.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
{
    using static TestDataManager;
    using static Testing;
    public class CreateMeetingTests : TestBase
    {
        private CreateMeetingCommand _command => new CreateMeetingCommand()
        {
            Title = "Test Title",
            AllowRsvp = false,
            EndTime = DateTime.Now,
            Footer = "Test Footer",
            Intro = "test intro",
            IsDraft = true,
            MaxRsvp = 10,
            MeetingHostBody = "Test meeting host body",
            PublishStartTime = DateTime.Now,
            StartTime = DateTime.Now,
            VimeoId = "1234",

        };

        private async Task<CreateMeetingCommand> SetupTestData()
        {
            var host = await CreateTestHost();

            var testCommand = _command;
            testCommand.MeetingHostId = host.Id;

            var firstSponsor = CreateTestSponsor();
            var secondSponsor = CreateTestSponsor();

            testCommand.MeetingSponsors = new List<CreateMeetingCommand.MeetingSponsor>()
            {
                new CreateMeetingCommand.MeetingSponsor(){SponsorId = firstSponsor.Id, Body = "First Sponsor Body"},
                new CreateMeetingCommand.MeetingSponsor(){SponsorId = secondSponsor.Id, Body = "Second Sponsor Body"}
            };

            return testCommand;
        }

        [Test]
        public async Task ShouldCreateTheMeeting()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

            var host = await CreateTestHost();

            var testCommand = _command;
            testCommand.MeetingHostId = host.Id;

            var createdId = await SendAsync(testCommand);

            var dbMeeting = await FindAsync<Domain.Entities.Meeting>(createdId);

            dbMeeting.Title.Should().Be(testCommand.Title);
            dbMeeting.AllowRsvp.Should().Be(testCommand.AllowRsvp);
            dbMeeting.EndTime.Should().Be(testCommand.EndTime);
            dbMeeting.Footer.Should().Be(testCommand.Footer);
            dbMeeting.Intro.Should().Be(testCommand.Intro);
            dbMeeting.IsDraft.Should().Be(testCommand.IsDraft);
            dbMeeting.MaxRsvp.Should().Be(testCommand.MaxRsvp);
            dbMeeting.HostMeetingBody.Should().Be(testCommand.MeetingHostBody);
            dbMeeting.PublishStartTime.Should().Be(testCommand.PublishStartTime);
            dbMeeting.StartTime.Should().Be(testCommand.StartTime);
            dbMeeting.VimeoId.Should().Be(testCommand.VimeoId);
            dbMeeting.MeetingHostId.Should().Be(testCommand.MeetingHostId);
        }

        [Test]
        public async Task ShouldCreateSponsorsWithTheMeeting()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

            var testCommand = await SetupTestData();

            var createdId = await SendAsync(testCommand);

            var dbMeetingSponsors = (await FindAllAsync<MeetingSponsor>()).ToArray();

            dbMeetingSponsors
                .Should().Contain(w => w.MeetingId == createdId && w.SponsorId == testCommand.MeetingSponsors.First().SponsorId)
                .Which.MeetingSponsorBody.Should()
                .Be(testCommand.MeetingSponsors.First().Body);

            dbMeetingSponsors
                .Should().Contain(w => w.MeetingId == createdId && w.SponsorId == testCommand.MeetingSponsors.Skip(1).First().SponsorId)
                .Which.MeetingSponsorBody.Should()
                .Be(testCommand.MeetingSponsors.Skip(1).First().Body);

        }



    }
}
