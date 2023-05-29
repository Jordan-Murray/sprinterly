using System.Text.Json.Serialization;

namespace Sprinterly.Models.Teams
{
    public class TeamDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
