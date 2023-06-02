using Sprinterly.Models.Teams;

namespace Sprinterly.Models
{
    public class TeamStats
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TeamMember> Members { get; set; }
        public string CurrentSprint { get; set; }
    }
}
