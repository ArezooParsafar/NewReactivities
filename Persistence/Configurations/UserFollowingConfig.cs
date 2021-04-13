using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserFollowingConfig : IEntityTypeConfiguration<UserFollowing>
    {
        public void Configure(EntityTypeBuilder<UserFollowing> builder)
        {
            builder.HasKey(k => new { k.ObserverId, k.TargetId });
            builder.HasOne(x => x.Observer)
                .WithMany(x => x.Followings)
                .HasForeignKey(f => f.ObserverId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Target)
                .WithMany(x => x.Followers)
                .HasForeignKey(f => f.TargetId)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
