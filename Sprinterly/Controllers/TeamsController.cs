using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{project}/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetTeams([FromRoute] string organization, [FromRoute] string project)
        {
            var teams = await _teamsService.FetchTeamsAsync(organization, project);

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetTeams([FromRoute] string organization, [FromRoute] string project,
            [FromRoute] string teamId)
        {
            var teams = await _teamsService.GetTeamAsync(organization, project, teamId);

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }
    }
}
