using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class AreaPathNode
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }
        
        [JsonPropertyName("children")]
        public List<AreaPathNode> Children { get; set; }
    }
}
