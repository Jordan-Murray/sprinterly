using System.Text.Json.Serialization;

namespace Sprinterly.Models.Sprints
{
    public class SprintAttributes
    {
        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("finishDate")]
        public string FinishDate { get; set; }

        [JsonPropertyName("timeFrame")]
        public string TimeFrame { get; set; }
    }
}
