using Domain.AuditableEntity;
using Domain.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Activity : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public DateTime HoldingDate { get; set; }
        public bool IsDeleted { get; set; }
        public ActivityCategory Category { get; set; }
        public int CategoryId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public virtual ICollection<Attendee> Attendees { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<UserPhoto> UserPhotos { get; set; }

    }
}
