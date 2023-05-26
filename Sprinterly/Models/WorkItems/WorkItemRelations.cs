using System.Text.Json.Serialization;

namespace Sprinterly.Models.WorkItems
{
    public class WorkItemRelations
    {
        [JsonPropertyName("rel")]
        public string RelationshipType { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}