namespace UserGroups.Domain.Entities
{
    public class MeetingSponsor
    {
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public int? SponsorId { get; set; }
        public Sponsor Sponsor { get; set; }

        public string MeetingSponsorBody { get; set; }
    }
}