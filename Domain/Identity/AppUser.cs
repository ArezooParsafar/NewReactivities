using Domain.AuditableEntity;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Identity
{
    public class AppUser : IdentityUser, IAuditableEntity
    {
        public AppUser()
        {
            UserTokens = new HashSet<UserToken>();
        }

        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public bool IsPublicProfile { get; set; }
        public bool IsActive { get; set; }
        public string SerialNumber { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Attendee> Attendees { get; set; }
        public virtual ICollection<UserFollowing> Followers { get; set; }
        public virtual ICollection<UserFollowing> Followings { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<UserPhoto> UserPhotos { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<UserUsedPassword> UserUsedPasswords { get; set; }



    }
}
