using Domain.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class RoleClaim : IdentityRoleClaim<string>, IAuditableEntity
    {
        public virtual Role Role { get; set; }
    }
}
