using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class MeetingSponsorConfiguration : IEntityTypeConfiguration<MeetingSponsor>
    {
        public void Configure(EntityTypeBuilder<MeetingSponsor> builder)
        {
            builder.HasKey(es => new {es.MeetingId, es.SponsorId});
        }
    }
}