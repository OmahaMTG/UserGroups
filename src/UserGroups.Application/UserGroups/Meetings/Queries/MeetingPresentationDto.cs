using System.Collections.Generic;
using AutoMapper;
using UserGroups.Application.Common.Mappings;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    public class MeetingPresentationDto : IMapFrom<Presentation>
    {
        public int PresentationId { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }
        public string VimeoId { get; set; }

        public IEnumerable<MeetingPresentationPresenterDto> MeetingPresentationPresenters { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Presentation, MeetingPresentationDto>()
                .ForMember(d => d.Body, opt => opt.MapFrom(p => p.Details))
                .ForMember(d => d.MeetingPresentationPresenters, opt => opt.MapFrom(p => p.PresentationPresenters))

                .ForMember(d => d.PresentationId, opt => opt.MapFrom(p => p.Id))
                ;
        }
    }
}
