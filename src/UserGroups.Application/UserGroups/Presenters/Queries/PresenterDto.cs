using System;
using UserGroups.Application.Common.Mappings;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Presenters.Queries
{
    public class PresenterDto : IMapFrom<Presenter>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ContactInfo { get; set; }
        public string OmahaMtgUserId { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByUser { get; set; }
        public string UpdatedByUser { get; set; }
    }
}
