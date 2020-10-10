﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UserGroups.Application.Common.Mappings;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UserGroups.Meetings.Queries
{
    public class MeetingDto : IMapFrom<Meeting>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MaxRsvp { get; set; }
        public Boolean AllowRsvp { get; set; }
        public string Intro { get; set; }
        public string Footer { get; set; }
        public bool IsDraft { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<string> Tags { get; set; }
        public string VimeoId { get; set; }
        public IEnumerable<MeetingSponsorDto> MeetingSponsors { get; set; }
        public int? MeetingHostId { get; set; }

        public string MeetingHostName { get; set; }
        public string HostMeetingBody { get; set; }

        // public IEnumerable<MeetingPresentationDto> MeetingPresentations { get; set; }

        // public int RSVPCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Meeting, MeetingDto>()
                .ForMember(m => m.Tags, opt => opt.MapFrom(s => s.MeetingTags.Select(mt => mt.Tag.Name)))
                .ForMember(m => m.MeetingSponsors, opt => opt.MapFrom(m => m.MeetingSponsors.Select(ms => ms.Sponsor)));
        }
    }
}
