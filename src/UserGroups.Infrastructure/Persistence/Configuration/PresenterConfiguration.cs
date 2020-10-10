using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class PresenterConfiguration : IEntityTypeConfiguration<Presenter>
    {
        public void Configure(EntityTypeBuilder<Presenter> builder)
        {
            //throw new System.NotImplementedException();
        }
    }
}