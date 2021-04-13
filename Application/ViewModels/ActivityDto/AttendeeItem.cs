namespace Application.ViewModels.ActivityDto
{
    public class AttendeeItem
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }

        public bool IsHost { get; set; }
    }
}
