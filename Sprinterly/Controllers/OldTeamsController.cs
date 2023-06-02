using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{project}/[controller]")]
    [ApiController]
    public class OldTeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public OldTeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<string>>> GetTeams([FromRoute] string organization, [FromRoute] string project)
        {
            var teams = await _teamsService.FetchTeamsAsync(organization, project);

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }

        [HttpGet("{teamName}")]
        public async Task<ActionResult<IEnumerable<string>>> GetTeams([FromRoute] string organization, [FromRoute] string project,
            [FromRoute] string teamName)
        {
            var teams = await _teamsService.GetTeamAsync(organization, project, teamName);

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }
    }
}
