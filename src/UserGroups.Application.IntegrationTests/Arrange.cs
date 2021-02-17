using Bogus;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Entities;



namespace UserGroups.Application.IntegrationTests
{

    using static Testing;
    public class Arrange
    {
        public OmahaMtgUser ArrangedByUser { get; private set; }


        //not thread safe
        //todo make thread safe
        public async Task EnsureArrangeUserSet()
        {
            if (ArrangedByUser == null)
            {
                var user = await AddAsync(
                                new OmahaMtgUser()
                                {
                                    FirstName = "Arrange",
                                    LastName = "User"

                                });

                await AddAsync(new IdentityUserRole<string>()
                {
                    RoleId = ApplicationRoles.Admin.ToString(),
                    UserId = user.Id
                });


                SetCurrentUser(user.Id, new List<ApplicationRoles>() { ApplicationRoles.Admin });

                ArrangedByUser = user;
            }

        }


        public async Task<Meeting> CreateTestMeeting(bool isDeleted = false, bool isDraft = false,
            int maxRsvp = 0, bool allowRsvp = true, bool isPastEvent = false, bool isFuturePublish = false)
        {
            await EnsureArrangeUserSet();
            return await AddAsync(
                new Faker<Meeting>()
                    .RuleFor(h => h.Intro, b => b.Lorem.Paragraph())
                    .RuleFor(h => h.Title, t => t.Hacker.Phrase())
                    .RuleFor(h => h.VimeoId, m => m.Random.Hash())
                    .RuleFor(h => h.IsDeleted, isDeleted)
                    .RuleFor(h => h.IsDraft, isDraft)
                    .RuleFor(h => h.MaxRsvp, maxRsvp)
                    .RuleFor(h => h.AllowRsvp, allowRsvp)
                    .RuleFor(h => h.PublishStartTime, m => isFuturePublish ? m.Date.Future() : m.Date.Past())
                    .RuleFor(h => h.StartTime, m => isPastEvent ? m.Date.Past() : m.Date.Future())
                    .RuleFor(h => h.EndTime, m => isPastEvent ? m.Date.Past() : m.Date.Future())
                    .Generate()
            );
        }

        public async Task<OmahaMtgUser> CreateTestUser(IEnumerable<ApplicationRoles> roles)
        {
            await EnsureArrangeUserSet();
            return await AddAsync(
                new Faker<OmahaMtgUser>()
                    .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                    .RuleFor(u => u.LastName, f => f.Person.LastName)
                    .Generate()
            );
        }

        public async Task<Presenter> CreateTestPresenter(string bio = "", string name = "", bool isDeleted = false)
        {
            await EnsureArrangeUserSet();
            return await AddAsync(
                new Faker<Presenter>()
                    .RuleFor(h => h.Bio, b => bio.IsNullOrEmpty() ? b.Lorem.Paragraph() : bio)
                    .RuleFor(h => h.ContactInfo, c => c.Lorem.Paragraph())
                    .RuleFor(h => h.IsDeleted, d => isDeleted)
                    .RuleFor(h => h.Name, n => name.IsNullOrEmpty() ? n.Company.CompanyName() : name)
                    .Generate()
            );
        }

        public async Task<Host> CreateTestHost(string blurb = "", string name = "", bool isDeleted = false)
        {
            await EnsureArrangeUserSet();
            return await AddAsync(
                new Faker<Host>()
                    .RuleFor(h => h.Blurb, b => blurb.IsNullOrEmpty() ? b.Lorem.Paragraph() : blurb)
                    .RuleFor(h => h.ContactInfo, c => c.Lorem.Paragraph())
                    .RuleFor(h => h.IsDeleted, d => isDeleted)
                    .RuleFor(h => h.Name, n => name.IsNullOrEmpty() ? n.Company.CompanyName() : name)
                    .Generate()
            );
        }


        public async Task CreateTestMeetingRsvp(string userId, int meetingId)
        {
            await EnsureArrangeUserSet();
            await AddAsync(new MeetingRsvp() { MeetingId = meetingId, UserId = userId });
        }



    }
}
