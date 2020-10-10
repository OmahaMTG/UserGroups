using System;

namespace UserGroups.Domain.Entities
{
    public class MeetingRsvp
    {
        public string UserId { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public DateTime RsvpTime { get; set; }
    }
}