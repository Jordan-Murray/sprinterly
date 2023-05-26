using Sprinterly.Models.Teams;

namespace Sprinterly.Services.Interfaces
{
    public interface ITeamsService
    {
        public Task<IEnumerable<Team>> FetchTeamsAsync(string organization, string project);
        public Task<IEnumerable<string>> FetchAreaPathsForTeam(string organization, string project, string teamName);
        public Task<Team> GetTeamAsync(string organization, string project, string teamId);
    }
}
