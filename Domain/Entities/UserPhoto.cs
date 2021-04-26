using Domain.Enums;
using Domain.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class UserPhoto
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AppUserId { get; set; }
        public Guid? ActivityId { get; set; }
        public Activity Activity { get; set; }
        public AppUser AppUser { get; set; }
        public ImageType ImageType { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
