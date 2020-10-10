using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class PresentationConfiguration : IEntityTypeConfiguration<Presentation>
    {
        public void Configure(EntityTypeBuilder<Presentation> builder)
        {
            //throw new System.NotImplementedException();
        }
    }
}