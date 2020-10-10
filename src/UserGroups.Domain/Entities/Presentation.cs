using System.Collections.Generic;
using UserGroups.Domain.Common;

namespace UserGroups.Domain.Entities
{
    public class Presentation : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public Meeting Meeting { get; set; }
        public int MeetingId { get; set; }
        public ICollection<PresentationPresenter> PresentationPresenters { get; set; }
        public bool IsDeleted { get; set; }
        public string VimeoId { get; set; }
    }
}