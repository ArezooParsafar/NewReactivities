using Domain.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class UserClaim : IdentityUserClaim<string>, IAuditableEntity
    {
        public virtual AppUser User { get; set; }
    }
}
