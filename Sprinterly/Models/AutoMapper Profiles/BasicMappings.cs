using AutoMapper;
using Sprinterly.Models.Teams;

namespace Sprinterly.Models.AutoMapper_Profiles
{
    public class BasicMappings : Profile
    {
        public BasicMappings()
        {
            CreateMap<TeamDTO, Team>();
            CreateMap<ProjectDTO, Project>();
            CreateMap<TeamDTO, Team>();
            CreateMap<TeamMemberDTO, TeamMember>();
            CreateMap<TeamMemberDetailsDTO, TeamMember>();
        }
    }
}
