using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class MeetingTagConfiguration : IEntityTypeConfiguration<MeetingTag>
    {
        public void Configure(EntityTypeBuilder<MeetingTag> builder)
        {
            builder.HasKey(er => new {er.MeetingId, er.TagId});
        }
    }
}