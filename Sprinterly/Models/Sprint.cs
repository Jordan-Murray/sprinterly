using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class Sprint
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("attributes")]
        public SprintAttributes Attributes { get; set; }
    }
}
