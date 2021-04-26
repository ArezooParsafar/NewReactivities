using Domain.Identity;
using System;

namespace Domain.Entities
{
    public class Attendee
    {
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
