using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class DevOpsDTO<T>
    {
        [JsonPropertyName("value")]
        public List<T> Value { get; set; } = new List<T>();
    }

    public class DevOpsValuesDTO<T>
    {
        [JsonPropertyName("values")]
        public List<T> Value { get; set; } = new List<T>();
    }
}
