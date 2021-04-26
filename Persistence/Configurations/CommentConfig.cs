using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Body)
                .HasMaxLength(1000)
                .IsRequired();

            builder.HasOne(x => x.Activity)
                .WithMany(x => x.Comments)
                .HasForeignKey(f => f.ActivityId);

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Comments)
                .HasForeignKey(f => f.AppUserId);

            builder.HasOne(x => x.UserPhoto)
                .WithMany(f => f.Comments)
                .HasForeignKey(x => x.UserPhotoId);

        }
    }
}
