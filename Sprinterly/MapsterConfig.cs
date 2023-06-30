using Mapster;
using Sprinterly.Models.Sprints;
using Sprinterly.Models.WorkItems;
using System.Reflection;

namespace Sprinterly
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<SprintDTO, Sprint>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Path, src => src.Path)
                .Map(dest => dest.Url, src => src.Url)
                .Map(dest => dest.StartDate, src => src.Attributes.StartDate)
                .Map(dest => dest.FinishDate, src => src.Attributes.FinishDate)
                .Map(dest => dest.TimeFrame, src => src.Attributes.TimeFrame);

            TypeAdapterConfig<WorkItemDTO, WorkItem>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Fields.Title)
                .Map(dest => dest.AssignedTo, src => src.Fields.AssignedTo)
                .Map(dest => dest.Type, src => src.Fields.WorkItemType)
                .Map(dest => dest.IterationPath, src => src.Fields.IterationPath)
                .Map(dest => dest.State, src => src.Fields.State)
                .Map(dest => dest.AreaPath, src => src.Fields.AreaPath)
                .Map(dest => dest.StoryPoints, src => src.Fields.StoryPoints)
                .PreserveReference(true);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
