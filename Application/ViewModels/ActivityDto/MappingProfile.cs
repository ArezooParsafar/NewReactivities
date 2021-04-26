using Application.ViewModels.CommentDto;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Linq;

namespace Application.ViewModels.ActivityDto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityItem>()
                .ForMember(c => c.CategoryName, o => o.MapFrom(s => s.Category.Title))
                .ForMember(c => c.ActivityId, o => o.MapFrom(x => x.Id))
                .ForMember(c => c.HostName, o => o.MapFrom(x => x.AppUser.DisplayName))
                .ForMember(c => c.MonthDiff, o => o.MapFrom<MonthDiffResolver>());

            CreateMap<Attendee, AttendeeItem>()
                .ForMember(x => x.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(x => x.ProfileImage, o => o.MapFrom(s => s.AppUser.UserPhotos.FirstOrDefault(x => x.ImageType == ImageType.Profile)))
                .ForMember(x => x.IsHost, o => o.MapFrom(s => s.Activity.AppUserId == s.AppUserId));

            CreateMap<Comment, CommentItem>()
                    .ForMember(x => x.CommentId, o => o.MapFrom(c => c.Id))
                    .ForMember(x => x.DisplayName, o => o.MapFrom(c => c.AppUser.DisplayName));


        }
    }
}
