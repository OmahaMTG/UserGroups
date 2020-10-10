using AutoMapper;
using UserGroups.Application.Common.Mappings;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    public class MeetingSponsorDto : IMapFrom<Sponsor>
    {
        public int? SponsorId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sponsor, MeetingSponsorDto>()
                .ForMember(d => d.Body, opt => opt.MapFrom(p => p.Blurb))
                  .ForMember(d => d.SponsorId, opt => opt.MapFrom(p => p.Id))
                ;
        }
    }
}
