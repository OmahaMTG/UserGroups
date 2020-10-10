namespace UserGroups.Domain.Entities
{
    public class MeetingTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}