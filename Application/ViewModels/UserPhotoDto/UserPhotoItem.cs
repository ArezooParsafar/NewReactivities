using Domain.Enums;
using System;

namespace Application.ViewModels.UserPhotoDto
{
    public class UserPhotoItem
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActivityTitle { get; set; }
        public ImageType ImageType { get; set; }
        public string Description { get; set; }
    }
}
