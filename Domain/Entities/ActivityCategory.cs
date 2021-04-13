using Domain.AuditableEntity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ActivityCategory : IAuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
