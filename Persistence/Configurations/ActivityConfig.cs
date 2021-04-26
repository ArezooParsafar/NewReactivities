using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ActivityConfig : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasIndex(x => x.Title);

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.City)
                .IsRequired().HasMaxLength(50);

            builder.Property(x => x.Venue)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Activities)
                .HasForeignKey(f => f.AppUserId);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Activities)
                .HasForeignKey(f => f.CategoryId);


        }
    }
}
