using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ActivityCategoryConfig : IEntityTypeConfiguration<ActivityCategory>
    {
        public void Configure(EntityTypeBuilder<ActivityCategory> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
