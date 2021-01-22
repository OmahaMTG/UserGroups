using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.TestData
{
    using static Testing;
    public class EventTestDataManager
    {
        public static async Task<Meeting> CreateMarMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
        {
            return await AddAsync(new Meeting()
            {
                AllowRsvp = false,
                IsDraft = true,
                MeetingHostId = host.Id,
                HostMeetingBody = host.Blurb,
                Presentations = presentations,
                MeetingSponsors = sponsors.Select(s => new MeetingSponsor()
                {
                    SponsorId = s.Id,
                    MeetingSponsorBody = s.Blurb
                }),
                Footer = @"
Thanks,  
Matt Ruwe  
mruwe@omahamtg.com  
.NET User's Group Co-Leader

Brian Olson  
bolson@omahamtg.com  
.NET User's Group Co-Leader  

Join our slack channel!  
[https://bit.ly/2nIjSNB](https://bit.ly/2nIjSNB)
                        ",
                MaxRsvp = 0,
                //VimeoId = "1234",
                PublishStartTime = DateTime.Parse("2020-03-01T18:04:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("2020-03-26T18:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("2020-03-26T20:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "Omaha .NET User Group March Meeting",
                Intro = @"
Fellow .NET Users,

We certainly do live in interesting times.  Given all of the news around the Covid-19 pandemic, Brian and I have been discussing our options.  With so much uncertainty, we feel like it would be irresponsible of us to continue meeting in-person.  However, we are very committed to our community and at this point, we're planning to convert our in-person meetings to virtual meetings.  There are a lot of details to work out with this and I'll be communicating those over the next few days.  For now, here at the details that we have for our next meeting.

This is a virtual-only event, but we're going to try to make it feel as much like a regular meeting as is possible.  The Corona-virus is changing the world around us and we're rolling with the punches.  To that end, our sponsor, Advantage Tech, has generously agreed to provide 4 $50 gift cards to Amazon.  We'll be giving these out at some point in the stream.  We'll be choosing the winners randomly from the meeting channel on our [Slack Workspace](https://bit.ly/2nIjSNB).  Details will be provided in the #general channel about an hour before the meeting starts.  If you haven't joined our [Slack Workspace](https://bit.ly/2nIjSNB), this is as good a time as you'll find.  There are a lot of amazing people that participate there who work on things you would have never guessed were happening in Omaha.  At least, that's been my experience!

Join us around 5:30 in the Slack channel to start chatting with others about what's been happening just like we would before a ""normal"" meeting.  At about 6:00 we'll start the meeting.  Throughout the presentation, I would encourage you to ask questions in either the Slack channel or in the YouTube stream chat.  Brian and I will be watching these comments as they come in and communicating those to our presenter.  I'm sure we'll run into a few hiccups along the way as we get this all figured out, but I'm very excited about the possibilities that streaming our sessions presents.
                "
            });
        }



    }
}
