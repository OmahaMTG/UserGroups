using Bogus;
using Castle.Core.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.TestData
{
    using static Testing;
    public static class TestDataManager
    {
        public static async Task<Host> CreateTestHost(string blurb = "", string name = "", bool isDeleted = false)
        {
            return await AddAsync(
                new Faker<Host>()
                    .RuleFor(h => h.Blurb, b => blurb.IsNullOrEmpty() ? b.Lorem.Paragraph() : blurb)
                    .RuleFor(h => h.ContactInfo, c => c.Lorem.Paragraph())
                    .RuleFor(h => h.IsDeleted, d => isDeleted)
                    .RuleFor(h => h.Name, n => name.IsNullOrEmpty() ? n.Company.CompanyName() : name)
                    .Generate()
            );
        }

        public static async Task<Sponsor> CreateTestSponsor(string blurb = "", string name = "", bool isDeleted = false)
        {
            return await AddAsync(
                new Faker<Sponsor>()
                    .RuleFor(h => h.Blurb, b => blurb.IsNullOrEmpty() ? b.Lorem.Paragraph() : blurb)
                    .RuleFor(h => h.ContactInfo, c => c.Lorem.Paragraph())
                    .RuleFor(h => h.IsDeleted, d => isDeleted)
                    .RuleFor(h => h.Name, n => name.IsNullOrEmpty() ? n.Company.CompanyName() : name)
                    .RuleFor(h => h.IncludeInBannerRotation, false)
                    .RuleFor(h => h.ShortBlurb, n => name.IsNullOrEmpty() ? n.Lorem.Paragraph() : name)
                    .RuleFor(h => h.SponsorUrl, su => su.Internet.Url())

                    .Generate()
            );
        }

        public static async Task<Presenter> CreateTestPresenter(string bio = "", string name = "", bool isDeleted = false)
        {
            return await AddAsync(
                new Faker<Presenter>()
                    .RuleFor(h => h.Bio, b => bio.IsNullOrEmpty() ? b.Lorem.Paragraph() : bio)
                    .RuleFor(h => h.ContactInfo, c => c.Lorem.Paragraph())
                    .RuleFor(h => h.IsDeleted, d => isDeleted)
                    .RuleFor(h => h.Name, n => name.IsNullOrEmpty() ? n.Company.CompanyName() : name)
                    .Generate()
            );
        }

        public static Presentation CreateTestPresentation(IEnumerable<Presenter> presenters, bool isDeleted = false)
        {

            return new Faker<Presentation>()
                .RuleFor(h => h.Details, b => b.Lorem.Paragraph())
                .RuleFor(h => h.IsDeleted, d => isDeleted)
                .RuleFor(h => h.Title, t => t.Hacker.Phrase())
                .RuleFor(h => h.VimeoId, m => m.Random.Hash())
                .RuleFor(h => h.PresentationPresenters,
                    presenters.Select(s => new PresentationPresenter() { PresenterId = s.Id }).ToList())
                .Generate();

        }

        public static async Task<Meeting> CreateTestMeeting(IEnumerable<Presentation> presentations, IEnumerable<Sponsor> sponsors,
                                                            Host host, bool isDeleted = false, bool isDraft = false,
                                                            int maxRsvp = 0, bool allowRsvp = true, bool isPastEvent = false, bool isFuturePublish = false)
        {

            return await AddAsync(
                new Faker<Meeting>()
                    .RuleFor(h => h.Intro, b => b.Lorem.Paragraph())
                    .RuleFor(h => h.Title, t => t.Hacker.Phrase())
                    .RuleFor(h => h.VimeoId, m => m.Random.Hash())
                    .RuleFor(h => h.IsDeleted, isDeleted)
                    .RuleFor(h => h.IsDraft, isDraft)
                    .RuleFor(h => h.MaxRsvp, maxRsvp)
                    .RuleFor(h => h.AllowRsvp, allowRsvp)
                    .RuleFor(m => m.MeetingSponsors, ms => sponsors.Select(s => new MeetingSponsor() { SponsorId = s.Id, MeetingSponsorBody = ms.Lorem.Paragraph() }).ToList())
                    .RuleFor(m => m.Presentations, presentations.ToList())
                    .RuleFor(m => m.MeetingHostId, b => host?.Id)
                    .RuleFor(m => m.HostMeetingBody, f => f.Lorem.Paragraph())
                    .RuleFor(h => h.PublishStartTime, m => isFuturePublish ? m.Date.Future() : m.Date.Past())
                    .RuleFor(h => h.StartTime, m => isPastEvent ? m.Date.Past() : m.Date.Future())
                    .RuleFor(h => h.EndTime, m => isPastEvent ? m.Date.Past() : m.Date.Future())
                    .Generate()
            );
        }






    }
}
