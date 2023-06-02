using Sprinterly.Models.Teams;

namespace Sprinterly.Services.Interfaces
{
    public interface ITeamsService
    {
        public Task<IEnumerable<Team>> GetTeamsAsync(string organization, string projectId);
        public Task<Team?> GetTeamAsync(string organization, string projectId, string teamId);
        public Task<IEnumerable<string>> FetchAreaPathsForTeam(string organization, string projectId, string teamId);
    }
}
