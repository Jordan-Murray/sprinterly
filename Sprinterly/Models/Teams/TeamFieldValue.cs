using System.Text.Json.Serialization;

namespace Sprinterly.Models.Teams
{
    public class TeamFieldValue
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
