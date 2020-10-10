using System;
using UserGroups.Application.Common.Mappings;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Sponsors.Queries
{
    public class SponsorDto : IMapFrom<Sponsor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string ContactInfo { get; set; }
        public string ShortBlurb { get; set; }
        public bool IsDeleted { get; set; }
        public bool IncludeInBannerRotation { get; set; }
        public string SponsorUrl { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByUser { get; set; }
        public string UpdatedByUser { get; set; }
    }
}