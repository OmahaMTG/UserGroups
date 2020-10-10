using System.Collections.Generic;
using UserGroups.Domain.Common;

namespace UserGroups.Domain.Entities
{
    public class Sponsor : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }
        public string SponsorUrl { get; set; }
        public ICollection<MeetingSponsor> MeetingSponsors { get; set; }
        public bool IsDeleted { get; set; }
        public string ShortBlurb { get; set; }
        public bool IncludeInBannerRotation { get; set; }
    }
}