using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;

        public TeamsController(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetTeams(string organization, string project)
        {
            var teams = await _devOpsService.FetchTeamNamesAsync(organization, project);

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }

        [HttpGet ("/areapaths")]
        public async Task<ActionResult<IEnumerable<string>>> GetAreaPaths(string organization, string project, string teamName)
        {
            var teams = await _devOpsService.FetchAreaPathsForTeam(organization, project, teamName);

            if (teams == null)
            {
                return NotFound("Error fetching area paths for team.");
            }

            return Ok(teams);
        }
    }
}
