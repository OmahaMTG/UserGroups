using System;
using System.Collections.Generic;
using UserGroups.Domain.Common;

namespace UserGroups.Domain.Entities
{
    public class Post : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDraft { get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
    }
}