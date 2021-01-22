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

        public static async Task<Meeting> CreateFebMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                MaxRsvp = 2,
                //VimeoId = "1234",
                PublishStartTime = DateTime.Parse("2020-01-01T08:09:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("2020-02-27T18:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("2020-02-27T20:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "Omaha .NET User Group February Meeting",
                Intro = @"
Fellow .NET Users,

It's hard to believe, but it's already time for our next .NET User's Group meeting!  Before we get to the details for that, I have a few community announcements.  First, **Carl Franklin's Blazor Road Show** is coming to Omaha on March 11th.  If you haven't already, please make sure to [RSVP and check out the details here](https://omahamtg.com/Event/Details/1283).  Please note that the venue has changed since I first published the details.

Second, the second annual Connectaha conference is happening on March 27th.  I attended this conference last year and am happy to be a participant again this year.  I hope to see many of you there!  For more details, please checkout [https://connectaha.com/](https://connectaha.com/).

Finally, the next .NET User's Group meeting will take place on Thursday, February 27th, starting at 6:00 PM.  Food and drink will be served by our sponsor starting 15-20 minutes before the meeting.  Please take special note of the slight location change.  Farm Credit has updated its facility and we're going to be meeting in their new Visitor Center.  Normally, you would go to the north building, however, for this meeting you'll want to enter on the west side of the south building.  

After the meeting, please join us for more drinks and appetizers at [Buffalo Wings and Rings](https://goo.gl/maps/5667qjSTUgHTnvrg6) sponsored by our friends at Harbinger Partners.
                "
            });
        }

        public static async Task<Meeting> CreateJanMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                VimeoId = "392341977",
                PublishStartTime = DateTime.Parse("2020-01-01T08:06:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("020-02-06T18:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("2020-01-08T20:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "Omaha .NET User Group January Meeting",
                Intro = @"
Fellow .NET Users,

Welcome to 2020!  We have a year full of opportunities to learn and network with your community of .NET developers.  I really can't overstate how excited I am.  For the first time in our User Group's history, we have an entire year planned in advance.  Every month has a sponsor, host and most importantly, a speaker!  Our community is as strong as its ever been and we're going to take advantage of the opportunity to try some new things this year.  Be sure to stay connected via our web site and Slack.

We also have a special meeting happening on March 11th featuring Carl Franklin from the .NET Rocks podcast.  You can find more details about that [here](https://omahamtg.com/Event/Details/1283).  Richard and Carl visited Omaha in 2012 and were frankly blown away by the reception.  We had more than 200 people join us at that event and I'm looking forward to making this one even better!  Please make sure to clear your calendar on March 11th so you can join us for this free event.  For more information, check out the event page at [https://omahamtg.com/Event/Details/1283](https://omahamtg.com/Event/Details/1283).

For our ""January"" meeting (being held in February), I will be presenting on my experience building software. It is the product of 20 years of being paid to produce software and contains as many tips, tricks, and tidbits as I can fit into the time allotted. I hope to see you there! Our sponsor, Kiewit, has added an incentive for you to come in the form of a few giveaway prizes. At the end of the presentation, we'll be giving away **2 $50 Amazon gift cards and a Nintendo Switch**!  We'll also be giving away a few of the new Omaha .NET t-shirts. Finally, the meeting is being held at Kiewit's University location which has a cafeteria where they'll be serving dinner.

Now,
                onto the details for our first meeting of 2020.The next.NET User Group meeting will take place on Thursday, February 6th, starting at 6:00 PM.PLEASE NOTE:  This is a week later than our regularly scheduled meetings since our host was not available until then.Food and drink will be served by our sponsor starting 15 - 20 minutes before the meeting.After the meeting, please join us at[Rocco’s Pizza, 1302 Mike Fahey St](https://goo.gl/maps/pMUfJVRVmx5rCMP67) for more appetizers, drinks, and conversations provided by our friends at Harbinger Partners.
                           "
            });
        }

        public static async Task<Meeting> CreateNovDecMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                VimeoId = "378677919",
                PublishStartTime = DateTime.Parse("2019-11-03T05:33:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("2019-12-05T18:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("2019-12-05T20:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "Omaha .NET User Group November/December Meeting",
                Intro = @"
Fellow .NET Users,

I have a few comments to start off the November/December meeting announcement.  First, we just completed our 2020 sponsorship drive and it was a huge success!  We have sold every sponsorship option available.  We have an amazing network of companies in Omaha that support us in a big way.  I’ll make a special point to call out our 2019 sponsors in my yearly wrap-up coming in a few weeks and will announce our 2020 sponsors in that email as well.

Since we have such a great group of sponsors who support the community, we would like to add another 5 or 6 meetings to the year.  However, this will add a fair amount of work to running the user’s group, so I want to make sure we have all of the “normal” monthly slots filled with speakers.  Right now, we have 9 of the 11 slots for 2020 filled.  If we can fill the two remaining slots by the end of 2019, we’ll commit to adding 5 or 6 meetings to 2020.  These meetings would most likely be held over the lunch hour.  We would love to add you to our speaker list in 2020.

Also, since we knew there would be a lot of interest in sponsorships, we decided to add a new option allowing additional sponsors to get involved.  We’re in the process of getting .NET User’s Group t-shirts printed.  We plan to give these out at our monthly meetings over the course of the coming year.  More details to come!

Okay, almost done…  For those of you interested in upping you community involvement, the Nebraska Code call for presenters is currently open.  You can check out the details [here](https://www.amegala.com/#/cfp/nebraskacode/2020).

Finally, the Connectaha conference call for presenters is also open.  You can get the details for that [here](https://www.papercall.io/connectaha-2020).

Now, on to this month’s meeting announcement!  The next .NET UG meeting will take place on Thursday, December 5th, starting at 6:00 PM.  Food and drink will be served by our sponsor starting 15-20 minutes before the start of the meeting.

Our sponsor, BCBS, wanted me to let you know that they will be serving Qdoba and offering a second-generation Echo Show as a giveaway prize!  You must be present to win.

After the meeting join us for more drinks, appetizers and engaging conversations hosted by our friends at Dynamo!  We'll be at Dudley's [2110 S 67th St](https://goo.gl/maps/96JAFxjqR5LMe5zU6).
                "
            });
        }

        public static async Task<Meeting> CreateOctMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                PublishStartTime = DateTime.Parse("2019-10-01T05:40:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("2019-10-24T18:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("2019-10-24T20:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "Omaha .NET User Group October Meeting",
                Intro = @"
Fellow .NET Users,

As a community of programming enthusiasts, it’s easy to forget about the non-technical components of our group.  We tend to focus on the web site, presentation topics, video hosting or technical discussions on our Slack workspace which I believe are essential to the continued success of the group.  However, there are a lot of non-technical components that are good to consider.  

For instance, it's important for our members to get to know one another and the types of problems we’re working on every day.  Some of my best professional friendships have come from stopping to talk to a stranger at the user group about what they’re working on.  Over time, conversations continue and we mutually benefit from the shared experience of understanding an alternative approach to solving problems or a different problem set altogether.

I would like to encourage each of you to get to know someone new from the user group and the types of problems they are solving.  It’s hard for Brian or me to have in-depth conversations at a User Group meeting, but email, Slack, Twitter, etc. are all great ways to tell us about your challenging problems.  I would love to hear about it!

Now, on to this month’s meeting announcement!  The next .NET UG meeting will take place on Thursday, October 24th, starting at 6:00 PM.  Please note that due to Halloween falling on the same day as our typically scheduled meeting, we moved this month's meeting forward one week.  Food and drink will be served by our sponsor starting 15-20 minutes before the start of the meeting.

Our sponsor, Advantage Tech, wanted me to let you know that they will be serving Qdoba and offering a $100 and 2 $50 gift cards as giveaway prizes!  You must be present to win.

After the meeting join us for more drinks, appetizers and engaging conversation at Cruisers Bar & Grill ([8634 F St](https://goo.gl/maps/b2HapqX1Pp596FpUA)) hosted by our friends at Dynamo!
                "
            });
        }

        public static async Task<Meeting> CreateSepMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                VimeoId = "364174242",
                PublishStartTime = DateTime.Parse("2019-09-01T07:15:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("2019-09-26T18:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("2019-09-26T20:00:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "Omaha .NET User Group September Meeting",
                Intro = @"
Fellow .NET Users,

It was great to see so many of you at the Heartland Developers Conference a couple of weeks ago.  For those of you that I didn't get a chance to talk to, make sure to come to our next .NET user's group meeting and say hi.  Also, [.NET Conf is happening next week](https://www.dotnetconf.net/) where Microsoft will be releasing .NET Core 3.0.  This is a free 3-day online conference that everyone who is programming .NET will be interested in.  

The next .NET UG meeting will take place on Thursday, September 26th, starting at 6:00 PM.  Food and drink will be served by our sponsor starting 15-20 minutes before the start of the meeting.

After the meeting join us at [Local Beer, Patio and Kitchen](https://goo.gl/maps/KHq3o3BcoSwd1yih8) for more drinks, appetizers and engaging conversation hosted by our friends at Dynamo!
                "
            });
        }

        public static async Task<Meeting> CreateAugMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                PublishStartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "",
                Intro = @"

                "
            });
        }

        public static async Task<Meeting> CreateJulMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                PublishStartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "",
                Intro = @"

                "
            });
        }

        public static async Task<Meeting> CreateJunMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                PublishStartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "",
                Intro = @"

                "
            });
        }

        public static async Task<Meeting> CreateMayMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                PublishStartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "",
                Intro = @"

                "
            });
        }

        public static async Task<Meeting> CreateAprMeeting(IEnumerable<Sponsor> sponsors, IEnumerable<Presentation> presentations, Host host)
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
                PublishStartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                StartTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EndTime = DateTime.Parse("", null, System.Globalization.DateTimeStyles.RoundtripKind),
                Title = "",
                Intro = @"

                "
            });
        }


    }
}
