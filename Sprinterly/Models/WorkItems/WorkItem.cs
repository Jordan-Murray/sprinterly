using System.Text.Json.Serialization;

namespace Sprinterly.Models.WorkItems
{
    public class WorkItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fields")]
        public WorkItemDetailsFields Fields { get; set; }

        public float HoursSpent { get; set; }
    }
}
