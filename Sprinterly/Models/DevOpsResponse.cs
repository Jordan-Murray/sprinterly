using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class DevOpsResponse<T>
    {
        [JsonPropertyName("value")]
        public List<T> Value { get; set; } = new List<T>();
    }
}
