using System.Text.Json.Serialization;

namespace Sprinterly.Models.Teams
{
    public class TeamMemberDTO
    {
        [JsonPropertyName("identity")]
        public TeamMemberDetailsDTO Details { get; set; }
    }

    public class TeamMemberDetailsDTO
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
