using Sprinterly.Models.WorkItems;

namespace Sprinterly.Services.Interfaces
{
    public interface ISprintStatsService
    {
        public float CalculateVelocity(List<WorkItemDTO> workItemDetails);
        public int GetNumberOfBugs(List<WorkItemDTO> workItemDetails);
        public int GetNumberOfIssues(List<WorkItemDTO> workItemDetails);
        public int GetNumberOfUserStories(List<WorkItemDTO> workItemDetails);
    }
}