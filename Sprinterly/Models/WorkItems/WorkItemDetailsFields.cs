using System.Text.Json.Serialization;

namespace Sprinterly.Models.WorkItems
{
    public class WorkItemDetailsFields
    {
        [JsonPropertyName("Microsoft.VSTS.Scheduling.StoryPoints")]
        public float StoryPoints { get; set; }

        [JsonPropertyName("System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonPropertyName("System.TeamProject")]
        public string TeamProject { get; set; }

        [JsonPropertyName("System.IterationPath")]
        public string IterationPath { get; set; }

        [JsonPropertyName("System.WorkItemType")]
        public string WorkItemType { get; set; }

        [JsonPropertyName("System.State")]
        public string State { get; set; }

        [JsonPropertyName("System.Reason")]
        public string Reason { get; set; }

        [JsonPropertyName("System.AssignedTo")]
        public AssignedTo AssignedTo { get; set; }

        [JsonPropertyName("System.CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("System.ChangedDate")]
        public DateTime ChangedDate { get; set; }

        [JsonPropertyName("System.CommentCount")]
        public int CommentCount { get; set; }

        [JsonPropertyName("System.Title")]
        public string Title { get; set; }

        [JsonPropertyName("System.BoardColumn")]
        public string BoardColumn { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.StateChangeDate")]
        public DateTime StateChangeDate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.ActivatedDate")]
        public DateTime ActivatedDate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.ResolvedDate")]
        public DateTime ResolvedDate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.ClosedDate")]
        public DateTime ClosedDate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.ClosedBy")]
        public ClosedBy ClosedBy { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.Priority")]
        public int Priority { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.StackRank")]
        public double StackRank { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.ValueArea")]
        public string ValueArea { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Build.IntegrationBuild")]
        public string IntegrationBuild { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.Risk")]
        public string Risk { get; set; }
    }

    public class AssignedTo
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

    public class ClosedBy
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
