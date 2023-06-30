using Sprinterly.Models.Teams;

namespace Sprinterly.Services.Interfaces
{
    public interface ITeamsService
    {
        public Task<List<TeamMember>> PopulateTeamMembersWithStats(string organization, string projectId, string teamId,
            List<TeamMember> teamMembers, string sprintId);
        public Task<IEnumerable<Team>> GetTeamsAsync(string organization, string projectId);
        public Task<Team?> GetTeamAsync(string organization, string projectId, string teamId, string sprintId);
        public Task<IEnumerable<string>> FetchAreaPathsForTeam(string organization, string projectId, string teamId);
    }
}
