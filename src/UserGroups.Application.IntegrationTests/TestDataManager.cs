using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests
{
    using static Testing;
    public class TestDataManager
    {
        public static async Task<Host> CreateTestHost(string blurb = "Test Blurb", string name = "Test Name", string contactInfo = "Test Contact Info", bool deleted = false)
        {
            return await AddAsync(new Host
            {
                Blurb = blurb,
                ContactInfo = contactInfo,
                IsDeleted = deleted,
                Name = name,
            });
        }

        public static async Task<Presenter> CreateTestPresenter(string name = "Test Name", string contactInfo = "TestContactInfo", string bio = "Test Bio", bool deleted = false)
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = contactInfo,
                IsDeleted = deleted,
                Name = name,
                Bio = bio
            });
        }

        public static async Task<Sponsor> CreateTestSponsor(string name = "Test Name")
        {
            return await AddAsync(new Sponsor
            {
                Blurb = "Test Blurb",
                ContactInfo = "Test Contact Info",
                IncludeInBannerRotation = true,
                IsDeleted = false,
                Name = name,
                ShortBlurb = "Test Short Blurb",
                SponsorUrl = "http://test.url"
            });
        }

        public static async Task<Meeting> CreateTestMeeting()
        {
            var firstSponsor = await CreateTestSponsor("First Sponsor");
            var secondSponsor = await CreateTestSponsor("Second Sponsor");
            var thirdSponsor = await CreateTestSponsor("Third Sponsor");

            var firstPresentationFirstTestPresenter = await CreateTestPresenter(name: "First Presentation First Presenter");
            var firstPresentationSecondTestPresenter = await CreateTestPresenter(name: "First Presentation Second Presenter");
            var firstPresentationThirdTestPresenter = await CreateTestPresenter(name: "First Presentation Third Presenter");

            var secondPresentationFirstTestPresenter = await CreateTestPresenter(name: "Second Presentation First Presenter");
            var secondPresentationSecondTestPresenter = await CreateTestPresenter(name: "Second Presentation Second Presenter");
            var secondPresentationThirdTestPresenter = await CreateTestPresenter(name: "Second Presentation Third Presenter");

            var host = await CreateTestHost();

            return await AddAsync(new Meeting()
            {
                Title = "Test Title",
                AllowRsvp = false,
                EndTime = DateTime.Now,
                Footer = "Test Footer",
                Intro = "test intro",
                IsDraft = true,
                MaxRsvp = 10,
                HostMeetingBody = "Test meeting host body",
                PublishStartTime = DateTime.Now,
                StartTime = DateTime.Now,
                VimeoId = "1234",
                MeetingHostId = host.Id,
                MeetingSponsors = new List<MeetingSponsor>()
                {
                    new MeetingSponsor()
                        {SponsorId = firstSponsor.Id, MeetingSponsorBody = "First Sponsor Meeting Body"},
                    new MeetingSponsor()
                        {SponsorId = secondSponsor.Id, MeetingSponsorBody = "Second Sponsor Meeting Body"},
                    new MeetingSponsor()
                        {SponsorId = thirdSponsor.Id, MeetingSponsorBody = "Third Sponsor Meeting Body"},
                },
                Presentations = new List<Presentation>()
                {
                    new Presentation()
                    {
                        Title = "First Presentation Title",
                        Details = "First Presentation Details",
                        PresentationPresenters = new List<PresentationPresenter>()
                        {
                            new PresentationPresenter()
                            {
                                PresenterId = firstPresentationFirstTestPresenter.Id,
                                PresenterPresentationBody = "First Presenter, First Presentation Body",
                            },
                            new PresentationPresenter()
                            {
                                PresenterId = firstPresentationSecondTestPresenter.Id,
                                PresenterPresentationBody = "Second Presenter, First Presentation Body",
                            },
                            new PresentationPresenter()
                            {
                                PresenterId = firstPresentationThirdTestPresenter.Id,
                                PresenterPresentationBody = "Third Presenter, First Presentation Body",
                            }
                        }
                    },
                    new Presentation()
                    {
                        Title = "Second Presentation Title",
                        Details = "Second Presentation Details",
                        PresentationPresenters = new List<PresentationPresenter>()
                        {
                            new PresentationPresenter()
                            {
                                PresenterId = secondPresentationFirstTestPresenter.Id,
                                PresenterPresentationBody = "First Presenter, Second Presentation Body",
                            },
                            new PresentationPresenter()
                            {
                                PresenterId = secondPresentationSecondTestPresenter.Id,
                                PresenterPresentationBody = "Second Presenter, Second Presentation Body",
                            },
                            new PresentationPresenter()
                            {
                                PresenterId = secondPresentationThirdTestPresenter.Id,
                                PresenterPresentationBody = "Third Presenter, Second Presentation Body",
                            }
                        }
                    }
                }

            });
        }


    }
}
