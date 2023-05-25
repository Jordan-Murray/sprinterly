using System.Text.Json.Serialization;

namespace Sprinterly.Models.WorkItems
{
    public class WorkItemDTO
    {
        [JsonPropertyName("workItems")]
        public List<WorkItem> Value { get; set; } = new List<WorkItem>();
    }
}
