using AutoMapper;
using Sprinterly.Models.Sprints;

namespace Sprinterly.Models.AutoMapper_Profiles
{
    public class SprintProfile : Profile
    {
        public SprintProfile()
        {
            CreateMap<SprintDTO, Sprint>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Attributes.StartDate))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(src => src.Attributes.FinishDate))
                .ForMember(dest => dest.TimeFrame, opt => opt.MapFrom(src => src.Attributes.TimeFrame));
        }
    }
}
