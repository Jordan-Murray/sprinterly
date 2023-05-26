using System.Text.Json.Serialization;

namespace Sprinterly.Models.WorkItems
{
    public class WorkItemDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fields")]
        public WorkItemDetailsFields Fields { get; set; }

        [JsonPropertyName("relations")]
        public List<WorkItemRelations> Relations { get; set; }

        public float HoursSpent { get; set; }
    }
}
