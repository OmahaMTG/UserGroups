using System.Collections.Generic;
using UserGroups.Domain.Common;

namespace UserGroups.Domain.Entities
{
    public class Presenter : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ContactInfo { get; set; }

        public string OmahaMtgUserId { get; set; }
        public OmahaMtgUser User { get; set; }
        public ICollection<PresentationPresenter> PresentationPresenters { get; set; }
        public bool IsDeleted { get; set; }
    }
}
