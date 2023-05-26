using System.Text.Json.Serialization;

namespace Sprinterly.Models.WorkItems
{
    public class WorkItemListDTO
    {
        [JsonPropertyName("workItems")]
        public List<WorkItemDTO> Value { get; set; } = new List<WorkItemDTO>();
    }
}
