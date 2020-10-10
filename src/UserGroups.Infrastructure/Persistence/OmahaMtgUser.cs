using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence
{
    public class OmahaMtgUser : IdentityUser
    {
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreateDate { get; set; }

        public IEnumerable<Meeting> CreatedMeetings { get; set; }

        public IEnumerable<Meeting> UpdatedMeetings { get; set; }

        public IEnumerable<Host> CreatedHosts { get; set; }

        public IEnumerable<Host> UpdatedHosts { get; set; }

        public IEnumerable<Post> CreatedPosts { get; set; }

        public IEnumerable<Post> UpdatedPosts { get; set; }

        public IEnumerable<Presentation> CreatedPresentations { get; set; }

        public IEnumerable<Presentation> UpdatedPresentations { get; set; }

        public IEnumerable<Presenter> CreatedPresenters { get; set; }

        public IEnumerable<Presenter> UpdatedPresenters { get; set; }

        public IEnumerable<Sponsor> CreatedSponsors { get; set; }

        public IEnumerable<Sponsor> UpdatedSponsors { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int EmailFailures { get; set; }
        public bool IsUnsubscribed { get; set; }
    }
}