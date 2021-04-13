using System;

namespace Application.ViewModels.CommentDto
{
    public class CommentItem
    {
        public Guid CommentId { get; set; }
        public string Body { get; set; }
        public Guid ActivityId { get; set; }
        public string DisplayName { get; set; }
        public string AppUserId { get; set; }
        public string AppUserProfileImage { get; set; }
    }
}
