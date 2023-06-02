using Sprinterly.Models.Sprints;
using Sprinterly.Models;
using Sprinterly.Services.Interfaces;
using Mapster;
using Sprinterly.Models.Teams;

namespace Sprinterly.Services
{
    public class SprintService : ISprintService
    {
        IDevOpsService _devOpsService;

        public SprintService(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }
        public async Task<IEnumerable<Sprint>> GetSprintsForTeam(string organization, string projectId, string teamId)
        {
            var url = $"{organization}/{projectId}/{teamId}/_apis/work/teamsettings/iterations?api-version=7.0";

            var sprintsResult = await _devOpsService.MakeDevOpsCall<DevOpsDTO<SprintDTO>>(url);
            if (sprintsResult != null)
            {
                var sprints = sprintsResult.Adapt<IEnumerable<Sprint>>();
                return sprints;
            }
            else
            {
                return Enumerable.Empty<Sprint>();
            }
        }

        public Task<Team> PopulateTeamWithStats(Team team, string sprintId)
        {
            //var sprint
        }
    }
}
