using Domain.AuditableEntity;

namespace Domain.Identity
{
    public class UserUsedPassword : IAuditableEntity
    {
        public int Id { get; set; }

        public string HashedPassword { get; set; }

        public virtual AppUser User { get; set; }
        public string UserId { get; set; }
    }
}
