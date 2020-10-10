using AutoMapper;
using UserGroups.Application.Common.Mappings;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    public class MeetingPresentationPresenterDto : IMapFrom<PresentationPresenter>
    {
        public int? PresenterId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PresentationPresenter, MeetingPresentationPresenterDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(p => p.Presenter.Name))

                .ForMember(d => d.Body, opt => opt.MapFrom(p => p.PresenterPresentationBody))
                .ForMember(d => d.PresenterId, opt => opt.MapFrom(p => p.PresenterId))
                ;
        }
    }
}
