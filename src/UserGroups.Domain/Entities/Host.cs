using System.Collections.Generic;
using UserGroups.Domain.Common;

namespace UserGroups.Domain.Entities
{
    public class Host : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
        public bool IsDeleted { get; set; }
    }
}