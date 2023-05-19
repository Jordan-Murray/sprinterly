using Sprinterly.Models;

namespace Sprinterly.Services.Interfaces
{
    public interface IDevOpsService
    {
        public Task<IEnumerable<string>> FetchTeamNamesAsync(string organization, string project);
        public Task<IEnumerable<Sprint>> FetchSprintsAsync(string organization, string project);
        Task<IEnumerable<string>> FetchAreaPathsForTeam(string organization, string project, string teamName);
        Task<IEnumerable<int>> FetchWorkItemsAsync(string organization, string project, string sprintName, IEnumerable<string> areaPaths);
    }
}
