using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Host> Hosts { get; set; }

        DbSet<Meeting> Meetings { get; set; }

        DbSet<MeetingRsvp> MeetingRsvps { get; set; }

        DbSet<MeetingSponsor> MeetingSponsors { get; set; }

        DbSet<MeetingTag> MeetingTags { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<PostTag> PostTags { get; set; }

        DbSet<Presentation> Presentations { get; set; }

        DbSet<Presenter> Presenters { get; set; }

        DbSet<PresentationPresenter> PresentationPresenters { get; set; }

        DbSet<Sponsor> Sponsors { get; set; }

        DbSet<Tag> Tags { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}