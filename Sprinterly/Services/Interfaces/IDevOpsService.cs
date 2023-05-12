using Sprinterly.Models;

namespace Sprinterly.Services.Interfaces
{
    public interface IDevOpsService
    {
        public Task<IEnumerable<string>> FetchTeamNamesAsync();
        public Task<List<Sprint>> FetchSprintsAsync();
    }
}
