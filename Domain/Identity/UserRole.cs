using Domain.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class UserRole : IdentityUserRole<string>, IAuditableEntity
    {
        public virtual AppUser User { get; set; }

        public virtual Role Role { get; set; }
    }
}
