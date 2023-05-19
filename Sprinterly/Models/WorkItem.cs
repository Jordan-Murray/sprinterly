using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class WorkItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
