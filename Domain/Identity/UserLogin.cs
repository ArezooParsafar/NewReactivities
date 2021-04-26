using Domain.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class UserLogin : IdentityUserLogin<string>, IAuditableEntity
    {
        public virtual AppUser User { get; set; }
    }
}
