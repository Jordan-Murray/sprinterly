using System.Text.Json.Serialization;

namespace Sprinterly.Models.Teams
{
    public class Team
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

        public int NumberOfMembers { get; set; }   
    }
}
