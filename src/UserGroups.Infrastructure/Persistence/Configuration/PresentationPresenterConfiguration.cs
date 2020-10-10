using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserGroups.Domain.Entities;

namespace UserGroups.Infrastructure.Persistence.Configuration
{
    public class PresentationPresenterConfiguration : IEntityTypeConfiguration<PresentationPresenter>
    {
        public void Configure(EntityTypeBuilder<PresentationPresenter> builder)
        {
            builder.HasKey(pp => new {pp.PresentationId, pp.PresenterId});
        }
    }
}