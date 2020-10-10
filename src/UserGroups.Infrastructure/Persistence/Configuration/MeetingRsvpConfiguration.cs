using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class MeetingRsvpConfiguration : IEntityTypeConfiguration<MeetingRsvp>
    {
        public void Configure(EntityTypeBuilder<MeetingRsvp> builder)
        {
            builder.HasKey(_ => new {_.MeetingId, _.UserId});
        }
    }
}