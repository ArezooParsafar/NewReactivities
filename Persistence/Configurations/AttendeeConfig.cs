using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AttendeeConfig : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.HasKey(k => new { k.ActivityId, k.AppUserId });

            builder.HasOne(c => c.Activity)
                .WithMany(x => x.Attendees)
                .HasForeignKey(x => x.ActivityId);

            builder.HasOne(k => k.AppUser)
                .WithMany(x => x.Attendees)
                .HasForeignKey(f => f.AppUserId);


        }
    }
}
