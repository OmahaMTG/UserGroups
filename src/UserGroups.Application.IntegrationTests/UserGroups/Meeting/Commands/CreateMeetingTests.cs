using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroups.Application.Common.Exceptions;
using UserGroups.Application.Common.Models;
using UserGroups.Application.UserGroups.Meetings.Commands;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.UserGroups.Meeting.Commands
{
    using static TestDataManager;
    using static Testing;
    public class CreateMeetingTests : TestBase
    {
        [SetUp]
        public async Task SetupTestData()
        {
            _testHost = await CreateTestHost();

            _firstPresentationFirstTestPresenter = await CreateTestPresenter(name: "First Presentation First Presenter");
            _firstPresentationSecondTestPresenter = await CreateTestPresenter(name: "First Presentation Second Presenter");
            _firstPresentationThirdTestPresenter = await CreateTestPresenter(name: "First Presentation Third Presenter");

            _firstTestSponsor = await CreateTestSponsor("First Sponsor");
            _secondTestSponsor = await CreateTestSponsor("Second Sponsor");
            _thirdTestSponsor = await CreateTestSponsor("ThirdSponsor");
        }

        private Host _testHost;
        private Presenter _firstPresentationFirstTestPresenter;
        private Presenter _firstPresentationSecondTestPresenter;
        private Presenter _firstPresentationThirdTestPresenter;

        private Sponsor _firstTestSponsor;
        private Sponsor _secondTestSponsor;
        private Sponsor _thirdTestSponsor;

        private CreateMeetingCommand Command() => new CreateMeetingCommand()
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
            MeetingHostId = _testHost.Id,
            MeetingSponsors = new List<CreateMeetingCommand.MeetingSponsor>()
            {
                new CreateMeetingCommand.MeetingSponsor(){SponsorId = _firstTestSponsor.Id, Body = "First Sponsor Body"},
                new CreateMeetingCommand.MeetingSponsor(){SponsorId = _secondTestSponsor.Id, Body = "Second Sponsor Body"},
                new CreateMeetingCommand.MeetingSponsor(){SponsorId = _thirdTestSponsor.Id, Body = "Third Sponsor Body"}
            },
            MeetingPresentations = new List<CreateMeetingCommand.MeetingPresentation>()
            {
                new CreateMeetingCommand.MeetingPresentation()
                {
                    Body = "First Presentation Body",
                    Title = "First Presentation",
                    VimeoId = "1234",
                    MeetingPresentationPresenters = new List<CreateMeetingCommand.MeetingPresentationPresenter>()
                    {
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "First Presentation First Presenter Body",
                            PresenterId = _firstPresentationFirstTestPresenter.Id
                        },
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "First Presentation Second Presenter Body",
                            PresenterId = _firstPresentationSecondTestPresenter.Id
                        },
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "First Presentation Third Presenter Body",
                            PresenterId = _firstPresentationThirdTestPresenter.Id
                        }
                    }
                },
                new CreateMeetingCommand.MeetingPresentation()
                {
                    Body = "Second Presentation Body",
                    Title = "Second Presentation",
                    VimeoId = "1234",
                    MeetingPresentationPresenters = new List<CreateMeetingCommand.MeetingPresentationPresenter>()
                    {
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "Second Presentation First Presenter Body",
                            PresenterId = _firstPresentationFirstTestPresenter.Id
                        },
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "Second Presentation Second Presenter Body",
                            PresenterId = _firstPresentationSecondTestPresenter.Id
                        },
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "Second Presentation Third Presenter Body",
                            PresenterId = _firstPresentationThirdTestPresenter.Id
                        }
                    }
                },
                new CreateMeetingCommand.MeetingPresentation()
                {
                    Body = "Third Presentation Body",
                    Title = "Third Presentation",
                    VimeoId = "1234",
                    MeetingPresentationPresenters = new List<CreateMeetingCommand.MeetingPresentationPresenter>()
                    {
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "Third Presentation First Presenter Body",
                            PresenterId = _firstPresentationFirstTestPresenter.Id
                        },
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "Third Presentation Second Presenter Body",
                            PresenterId = _firstPresentationSecondTestPresenter.Id
                        },
                        new CreateMeetingCommand.MeetingPresentationPresenter()
                        {
                            Body = "Third Presentation Third Presenter Body",
                            PresenterId = _firstPresentationThirdTestPresenter.Id
                        }
                    }
                }

            }
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
            var command = Command();
            var createdId = await SendAsync(command);

            var dbMeeting = await FindAsync<Domain.Entities.Meeting>(createdId);
            AssertBasicInfo(dbMeeting, command);
            AssertSponsors((await FindAllAsync<MeetingSponsor>()).ToList());

            var dbMeetingPresentations = (await FindAllAsync<Presentation>("PresentationPresenters")).Where(w => w.MeetingId == createdId).ToArray();

            var firstDbMeetingPresentation =
                dbMeetingPresentations.FirstOrDefault(w => w.MeetingId == createdId && w.Title == command.MeetingPresentations[0].Title);
            AssertPresentations(firstDbMeetingPresentation, command.MeetingPresentations[0]);

            var secondDbMeetingPresentation =
                dbMeetingPresentations.FirstOrDefault(w => w.MeetingId == createdId && w.Title == command.MeetingPresentations[1].Title);
            AssertPresentations(secondDbMeetingPresentation, command.MeetingPresentations[1]);

            var thirdDbMeetingPresentation =
                dbMeetingPresentations.FirstOrDefault(w => w.MeetingId == createdId && w.Title == command.MeetingPresentations[2].Title);
            AssertPresentations(thirdDbMeetingPresentation, command.MeetingPresentations[2]);
        }

        private void AssertBasicInfo(Domain.Entities.Meeting actual, CreateMeetingCommand requested)
        {
            actual.Title.Should().Be(requested.Title);
            actual.AllowRsvp.Should().Be(requested.AllowRsvp);
            actual.EndTime.Should().Be(requested.EndTime);
            actual.Footer.Should().Be(requested.Footer);
            actual.Intro.Should().Be(requested.Intro);
            actual.IsDraft.Should().Be(requested.IsDraft);
            actual.MaxRsvp.Should().Be(requested.MaxRsvp);
            actual.HostMeetingBody.Should().Be(requested.MeetingHostBody);
            actual.PublishStartTime.Should().Be(requested.PublishStartTime);
            actual.StartTime.Should().Be(requested.StartTime);
            actual.VimeoId.Should().Be(requested.VimeoId);
            actual.MeetingHostId.Should().Be(requested.MeetingHostId);
            actual.CreatedByUser.Should().Be("UserId");
            actual.UpdatedByUser.Should().Be("UserId");
            actual.CreatedDate.Should().BeCloseTo(DateTime.Now, 1000);
            actual.UpdatedDate.Should().BeCloseTo(DateTime.Now, 1000);
            return;
        }

        private void AssertSponsors(IList<MeetingSponsor> actual)
        {
            actual
                .Should().Contain(w => w.SponsorId == _firstTestSponsor.Id)
                .Which.MeetingSponsorBody.Should()
                .Be("First Sponsor Body");

            actual
                .Should().Contain(w => w.SponsorId == _secondTestSponsor.Id)
                .Which.MeetingSponsorBody.Should()
                .Be("Second Sponsor Body");

            actual
                .Should().Contain(w => w.SponsorId == _thirdTestSponsor.Id)
                .Which.MeetingSponsorBody.Should()
                .Be("Third Sponsor Body");
        }

        private void AssertPresentations(Presentation actual, CreateMeetingCommand.MeetingPresentation command)
        {
            actual.Should().NotBeNull();
            actual.Details.Should().Be(command.Body);
            actual.VimeoId.Should().Be(command.VimeoId);
            actual.IsDeleted.Should().BeFalse();
            actual.CreatedDate.Should().BeCloseTo(DateTime.Now, 1000);
            actual.UpdatedDate.Should().BeCloseTo(DateTime.Now, 1000);
            actual.CreatedByUser.Should().Be("UserId");
            actual.UpdatedByUser.Should().Be("UserId");

            AssertPresenters(actual.PresentationPresenters.ToList(), command.MeetingPresentationPresenters.ToList());
        }

        private void AssertPresenters(IList<PresentationPresenter> actual, IList<CreateMeetingCommand.MeetingPresentationPresenter> command)
        {
            actual.Should().HaveCount(command.Count());

            actual
                .Select(s => new { body = s.PresenterPresentationBody })
                .Should()
                .BeEquivalentTo(command.Select(s => new { body = s.Body }));
        }

        [Test]
        public void ShouldThrowIfUserIsNotSponsorAdmin()
        {
            FluentActions.Invoking(() => SendAsync(Command())).Should().Throw<NotAuthorizedException>();
        }


    }
}
