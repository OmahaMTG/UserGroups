using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Application.Common.Interfaces;
using UserGroups.Application.Common.Models;
using UserGroups.Domain.Common;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<OmahaMtgUser>, IApplicationDbContext
    {
        private readonly ITimeUtility _timeUtility;
        private readonly IUserContext _userContext;
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions options, IUserContext userContext, ITimeUtility timeUtility, ILoggerFactory loggerFactory) :
            base(options)
        {
            _userContext = userContext;
            _timeUtility = timeUtility;
            _loggerFactory = loggerFactory;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //builder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Id = "admin",
            //    Name = "admin",
            //    NormalizedName = "admin"
            //});


            //https://stackoverflow.com/questions/50375357/how-to-create-a-table-corresponding-to-enum-in-ef-core-code-first
            //builder
            //    .Entity<OmahaMtgUser>()
            //    .Property(e => e.role)
            //    .HasConversion<int>();

            //builder
            //    .Entity<WineVariant>()
            //    .Property(e => e.WineVariantId)
            //    .HasConversion<int>();

            builder
                .Entity<IdentityRole>().HasData(
                    Enum.GetValues(typeof(ApplicationRoles))
                        .Cast<ApplicationRoles>()
                        .Select(e => new IdentityRole()
                        {
                            Id = e.ToString(),
                            Name = e.ToString(),
                            NormalizedName = e.ToString()
                        })
                );


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
                        entity.CreatedByUserId = currentUserId;
                        entity.CreatedDate = _timeUtility.GetCurrentSystemTime;
                    }

                    entity.UpdatedByUserId = currentUserId;
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
                        entity.CreatedByUserId = currentUserId;
                    //   entity.CreatedDate = now;

                    entity.UpdatedByUserId = currentUserId;
                    // entity.UpdatedDate = now;
                }


            return base.SaveChanges();
        }
    }
}
