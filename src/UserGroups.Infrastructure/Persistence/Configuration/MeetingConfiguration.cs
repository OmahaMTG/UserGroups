using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder
                .HasMany(m => m.MeetingTags).WithOne(mt => mt.Meeting).OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(m => m.MeetingSponsors).WithOne(mt => mt.Meeting).OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(m => m.Presentations).WithOne(mt => mt.Meeting).OnDelete(DeleteBehavior.Cascade);
        }
    }
}