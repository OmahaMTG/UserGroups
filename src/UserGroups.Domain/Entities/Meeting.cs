using System;
using System.Collections.Generic;
using UserGroups.Domain.Common;

namespace UserGroups.Domain.Entities
{
    public class Meeting : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public bool IsDraft { get; set; }

        public IEnumerable<MeetingTag> MeetingTags { get; set; }

        public string VimeoId { get; set; }
        public int? MaxRsvp { get; set; }
        public bool AllowRsvp { get; set; }
        public Host MeetingHost { get; set; }

        public ICollection<MeetingRsvp> MeetingRsvps { get; set; }
        public int? MeetingHostId { get; set; }
        public string HostMeetingBody { get; set; }

        public string Intro { get; set; }
        public string Footer { get; set; }
        public bool IsDeleted { get; set; }

        public int? OldMeetingId { get; set; }
        public IEnumerable<Presentation> Presentations { get; set; }

        public IEnumerable<MeetingSponsor> MeetingSponsors { get; set; }
    }
}