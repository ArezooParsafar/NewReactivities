using AutoMapper;
using Domain.Entities;

namespace Application.ViewModels.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActivityCategory, OptionData>()
            .ForMember(c => c.Key, o => o.MapFrom(x => x.Id))
            .ForMember(c => c.Label, o => o.MapFrom(x => x.Title))
            .ForMember(c => c.Value, o => o.MapFrom(x => x.Id));
        }
    }
}