using System;

namespace UserGroups.Domain.Common
{
    public class AuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByUser { get; set; }
        public string UpdatedByUser { get; set; }
    }
}