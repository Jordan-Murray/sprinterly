using System.Text.Json.Serialization;

namespace Sprinterly.Models.Sprints
{
    public class SprintDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("attributes")]
        public SprintAttributes Attributes { get; set; }
    }
}
