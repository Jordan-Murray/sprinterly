using AutoMapper;
using Sprinterly.Models.WorkItems;

namespace Sprinterly.Models.AutoMapper_Profiles
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<WorkItemDTO, WorkItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Fields.Title))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.Fields.AssignedTo.DisplayName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Fields.WorkItemType))
                .ForMember(dest => dest.IterationPath, opt => opt.MapFrom(src => src.Fields.IterationPath))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Fields.State))
                .ForMember(dest => dest.AreaPath, opt => opt.MapFrom(src => src.Fields.AreaPath))
                .ForMember(dest => dest.StoryPoints, opt => opt.MapFrom(src => src.Fields.StoryPoints))
                .PreserveReferences();
        }
    }
}
