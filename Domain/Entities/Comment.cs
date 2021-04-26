using Domain.AuditableEntity;
using Domain.Identity;
using System;

namespace Domain.Entities
{
    public class Comment : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Activity Activity { get; set; }
        public Guid? ActivityId { get; set; }
        public string UserPhotoId { get; set; }
        public UserPhoto UserPhoto { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
