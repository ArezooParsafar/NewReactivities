using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserPhotoConfig : IEntityTypeConfiguration<UserPhoto>
    {
        public void Configure(EntityTypeBuilder<UserPhoto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Path)
                .IsRequired();

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.UserPhotos)
                .HasForeignKey(f => f.AppUserId);

            builder.HasOne(c => c.Activity)
                .WithMany(x => x.UserPhotos)
                .HasForeignKey(f => f.ActivityId);

        }
    }
}
