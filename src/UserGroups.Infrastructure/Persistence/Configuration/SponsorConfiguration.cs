using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class SponsorConfiguration : IEntityTypeConfiguration<Sponsor>
    {
        public void Configure(EntityTypeBuilder<Sponsor> builder)
        {
            //throw new System.NotImplementedException();
        }
    }
}