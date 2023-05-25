using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Sprinterly.Models.WorkItems
{
    public class WorkItemDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fields")]
        public WorkItemDetailsFields Fields { get; set; }
    }
}