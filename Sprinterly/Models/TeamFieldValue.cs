using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class TeamFieldValue
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
