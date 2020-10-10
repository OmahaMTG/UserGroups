using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Domain.Common;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ITimeUtility _timeUtility;
        private readonly IUserContext _userContext;

        public ApplicationDbContext(DbContextOptions options, IUserContext userContext, ITimeUtility timeUtility) :
            base(options)
        {
            _userContext = userContext;
            _timeUtility = timeUtility;
        }

        public DbSet<Host> Hosts { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingRsvp> MeetingRsvps { get; set; }
        public DbSet<MeetingSponsor> MeetingSponsors { get; set; }
        public DbSet<MeetingTag> MeetingTags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Presenter> Presenters { get; set; }
        public DbSet<PresentationPresenter> PresentationPresenters { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            string currentUserId = null;

            if (_userContext.IsLoggedIn()) currentUserId = _userContext.UserId;

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            // var now = _timeUtility.CurrentTime();

            foreach (var entry in modifiedEntries)
                if (entry.Entity is AuditableEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedByUser = currentUserId;
                        entity.CreatedDate = _timeUtility.GetCurrentSystemTime;
                    }

                    entity.UpdatedByUser = currentUserId;
                    entity.UpdatedDate = _timeUtility.GetCurrentSystemTime;
                }


            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            string currentUserId = null;

            if (_userContext.IsLoggedIn()) currentUserId = _userContext.UserId;


            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            //var now = _timeUtility.CurrentTime();

            foreach (var entry in modifiedEntries)
                if (entry.Entity is AuditableEntity entity)
                {
                    if (entry.State == EntityState.Added)
                        entity.CreatedByUser = currentUserId;
                    //   entity.CreatedDate = now;

                    entity.UpdatedByUser = currentUserId;
                    // entity.UpdatedDate = now;
                }


            return base.SaveChanges();
        }
    }
}