namespace Application.ViewModels.UserDto
{
    public class UserProfileDetail
    {
        public string AppUserId { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public string HeaderImage { get; set; }
        public int FollowingsCount { get; set; }
        public int FollowersCount { get; set; }
        public int PendingRequestsCount { get; set; }
        public string Bio { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsPublicProfile { get; set; }
    }
}
