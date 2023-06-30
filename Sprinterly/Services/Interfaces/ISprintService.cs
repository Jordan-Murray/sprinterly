using Sprinterly.Models.Sprints;
using Sprinterly.Models.Teams;

namespace Sprinterly.Services.Interfaces
{
    public interface ISprintService
    {
        public Task<IEnumerable<Sprint>> GetSprintsForTeam(string organization, string projectId, string teamId);
    }
}