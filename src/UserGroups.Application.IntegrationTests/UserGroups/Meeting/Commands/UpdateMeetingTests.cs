using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.IntegrationTests.TestData;
using UserGroups.Application.UserGroups.Meetings.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
{
    using static TestDataManager;
    using static Testing;
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
            MeetingSponsors = new List<UpdateMeetingCommand.MeetingSponsor>(),
            MaxRsvp = 999,
            VimeoId = "UpdatedVimeo",
            MeetingHostId = null,
            MeetingHostBody = "Updated Host Body",
            MeetingPresentations = new List<UpdateMeetingCommand.MeetingPresentation>(),
            Tags = new List<string>()

        };

        //  private Update


        [Test]
        public async Task ShouldUpdateBaseMeetingDetails()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var testMeeting = await CreateTestMeeting(new List<Presentation>(), new List<Sponsor>(), null);
            var request = _command;
            request.Id = testMeeting.Id;
            await SendAsync(request);

            var updatedMeeting = await FindAsync<Domain.Entities.Meeting>(testMeeting.Id);

            updatedMeeting.AllowRsvp.Should().Be(request.AllowRsvp);
            updatedMeeting.StartTime.Should().Be(request.StartTime);
            updatedMeeting.EndTime.Should().Be(request.EndTime);
            updatedMeeting.PublishStartTime.Should().Be(request.PublishStartTime);
            updatedMeeting.Footer.Should().Be(request.Footer);
            updatedMeeting.Intro.Should().Be(request.Intro);
            updatedMeeting.IsDraft.Should().Be(request.IsDraft);
            updatedMeeting.MeetingHostId.Should().Be(request.MeetingHostId);
            updatedMeeting.HostMeetingBody.Should().Be(request.MeetingHostBody);
            updatedMeeting.Title.Should().Be(request.Title);
            updatedMeeting.VimeoId.Should().Be(request.VimeoId);
        }

        [Test]
        public async Task ShouldUpdateMeetingSponsors()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var firstSponsor = await CreateTestSponsor();
            var secondSponsor = await CreateTestSponsor();
            var thirdSponsor = await CreateTestSponsor();
            var testMeeting = await CreateTestMeeting(new List<Presentation>(), new List<Sponsor>() { firstSponsor, secondSponsor, thirdSponsor }, null);

            var request = _command;
            request.Id = testMeeting.Id;

            //remove the first, update the body on the second, remove the third, and add a new third
            var newThirdSponsor = await CreateTestSponsor();
            var newMeetingSponsors = new List<UpdateMeetingCommand.MeetingSponsor>
            {
                new UpdateMeetingCommand.MeetingSponsor() {Body = "New Body", SponsorId = secondSponsor.Id},
                new UpdateMeetingCommand.MeetingSponsor() {Body = newThirdSponsor.Blurb, SponsorId = newThirdSponsor.Id}
            };

            request.MeetingSponsors = newMeetingSponsors;
            await SendAsync(request);

            var firstMeetingSponsor = (await FindAllAsync<Domain.Entities.MeetingSponsor>())
                .FirstOrDefault(w => w.SponsorId == firstSponsor.Id & w.MeetingId == testMeeting.Id);

            firstMeetingSponsor.Should().Be(null);

            var secondMeetingSponsor = (await FindAllAsync<Domain.Entities.MeetingSponsor>())
                .FirstOrDefault(w => w.SponsorId == secondSponsor.Id & w.MeetingId == testMeeting.Id);

            secondMeetingSponsor.Should().NotBe(null);
            secondMeetingSponsor?.MeetingSponsorBody.Should().Be("New Body");

            var thirdMeetingSponsor = (await FindAllAsync<Domain.Entities.MeetingSponsor>())
                .FirstOrDefault(w => w.SponsorId == newThirdSponsor.Id & w.MeetingId == testMeeting.Id);

            thirdMeetingSponsor.Should().NotBe(null);
            thirdMeetingSponsor?.MeetingSponsorBody.Should().Be(newThirdSponsor.Blurb);

            //todo
        }

        [Test]
        public async Task ShouldUpdateMeetingPresentations()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

            var firstPresentation = CreateTestPresentation(new List<Presenter>());

            var secondPresentation = CreateTestPresentation(new List<Presenter>());

            var thirdPresentation = CreateTestPresentation(new List<Presenter>());

            var testMeeting = await CreateTestMeeting(new List<Presentation>() { firstPresentation, secondPresentation, thirdPresentation }, new List<Sponsor>(), null);

            var request = _command;
            request.Id = testMeeting.Id;

            var secondMeetingId =
                testMeeting.Presentations.FirstOrDefault(w => w.Title == secondPresentation.Title)?.Id;

            //remove the first, update the second, remove the third, and add a new third
            request.MeetingPresentations = new List<UpdateMeetingCommand.MeetingPresentation>()
            {
                new UpdateMeetingCommand.MeetingPresentation()
                {
                    Id = secondMeetingId,
                    Body = "Updated Body",
                    MeetingPresentationPresenters = new List<UpdateMeetingCommand.MeetingPresentationPresenter>(),
                    Title = "Updated Title",
                    VimeoId = "Updated Vimeo ID"
                },
                new UpdateMeetingCommand.MeetingPresentation()
                {
                    Body = "New Body",
                    MeetingPresentationPresenters = new List<UpdateMeetingCommand.MeetingPresentationPresenter>(),
                    Title = "New Title",
                    VimeoId = "New Vimeo ID"
                },
            };
            await SendAsync(request);

            var meetingPresentations = (await FindAllAsync<Domain.Entities.Presentation>()).ToList();

            meetingPresentations.Count(w => w.MeetingId == testMeeting.Id).Should().Be(2);

            var secondMeeting = await FindAsync<Presentation>(secondMeetingId);

            secondMeeting.Details.Should().Be("Updated Body");
            secondMeeting.Title.Should().Be("Updated Title");
            secondMeeting.VimeoId.Should().Be("Updated Vimeo ID");
        }

        [Test]
        public async Task ShouldUpdateMeetingPresentationsPresenters()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });

            var firstTestPresenter = await CreateTestPresenter();
            var secondTestPresenter = await CreateTestPresenter();
            var thirdTestPresenter = await CreateTestPresenter();

            var testPresentation = CreateTestPresentation(new List<Presenter>() { firstTestPresenter, secondTestPresenter, thirdTestPresenter });


            var testMeeting = await CreateTestMeeting(new List<Presentation>() { testPresentation }, new List<Sponsor>(), null);
            var request = _command;
            request.Id = testMeeting.Id;

            request.MeetingPresentations = new List<UpdateMeetingCommand.MeetingPresentation>()
            {
                new UpdateMeetingCommand.MeetingPresentation()
                {
                    Id = testMeeting.Id,
                    Body = "Updated Body",
                    MeetingPresentationPresenters = new List<UpdateMeetingCommand.MeetingPresentationPresenter>(),
                    Title = "Updated Title",
                    VimeoId = "Updated Vimeo ID"
                }
                new UpdateMeetingCommand.MeetingPresentation()
                {
                    Body = "New Body",
                    MeetingPresentationPresenters = new List<UpdateMeetingCommand.MeetingPresentationPresenter>(),
                    Title = "New Title",
                    VimeoId = "New Vimeo ID"
                },
            };


            await SendAsync(request);

            var updatedMeeting = await FindAsync<Domain.Entities.Meeting>(testMeeting.Id);

            //todo
        }

        [Test]
        public void ShouldThrowIfMeetingNotFound()
        {
            SetRoles(new List<ApplicationRoles> { ApplicationRoles.Admin });
            var command = _command;
            command.Id = 1;

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldThrowIfUserIsNotMeetingAdmin()
        {
            FluentActions.Invoking(() => SendAsync(_command)).Should().Throw<NotAuthorizedException>();
        }
    }
}
