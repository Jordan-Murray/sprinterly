using Mapster;
using Sprinterly.Models.Sprints;
using System.Reflection;

namespace Sprinterly
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<SprintDTO, Sprint>
                .NewConfig()
                .Map(dest => dest.StartDate, src => src.Attributes.StartDate)
                .Map(dest => dest.FinishDate, src => src.Attributes.FinishDate)
                .Map(dest => dest.TimeFrame, src => src.Attributes.TimeFrame);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
