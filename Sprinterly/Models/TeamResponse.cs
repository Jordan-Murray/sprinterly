using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class TeamResponse
    {
        [JsonPropertyName("value")]
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
