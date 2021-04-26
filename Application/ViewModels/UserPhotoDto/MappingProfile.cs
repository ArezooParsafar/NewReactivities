using AutoMapper;
using Domain.Entities;

namespace Application.ViewModels.UserPhotoDto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserPhoto, UserPhotoItem>()
                .ForMember(c => c.ActivityTitle, c => c.MapFrom(x => x.Activity != null ? x.Activity.Title : ""));

        }
    }
}
