using Sprinterly.Models.WorkItems;

namespace Sprinterly.Services.Interfaces
{
    public interface ISprintStatsService
    {
        public float CalculateVelocity(List<WorkItem> workItemDetails);
        public int GetNumberOfBugs(List<WorkItem> workItemDetails);
        public int GetNumberOfIssues(List<WorkItem> workItemDetails);
        public int GetNumberOfUserStories(List<WorkItem> workItemDetails);
    }
}