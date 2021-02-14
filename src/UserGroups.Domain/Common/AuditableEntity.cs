using System;
using UserGroups.Domain.Entities;

namespace UserGroups.Domain.Common
{
    public class AuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByUserId { get; set; }
        public string UpdatedByUserId { get; set; }
        public OmahaMtgUser CreatedByUser { get; set; }
        public OmahaMtgUser UpdatedByUser { get; set; }
    }
}
