using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{project}/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;

        public TeamsController(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetTeams([FromRoute] string organization, [FromRoute] string project)
        {
            var teams = await _devOpsService.FetchTeamNamesAsync(organization, project);

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }

        [HttpGet ("/areapaths")]
        public async Task<ActionResult<IEnumerable<string>>> GetAreaPaths(
            [FromRoute] string organization, [FromRoute] string project, [FromQuery] string teamName)
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
