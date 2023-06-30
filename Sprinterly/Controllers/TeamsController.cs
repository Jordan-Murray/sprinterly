using Microsoft.AspNetCore.Mvc;
using Sprinterly.Models.Teams;
using Sprinterly.Services;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{projectId}/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetTeams([FromRoute] string organization, [FromRoute] string projectId)
        {
            var teams = await _teamsService.GetTeamsAsync(organization, projectId);

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }

        [HttpGet("{teamId}/sprint/{sprintId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetTeam([FromRoute] string organization, [FromRoute] string projectId,
            [FromRoute] string teamId, [FromRoute] string sprintId)
        {
            var team = await _teamsService.GetTeamAsync(organization, projectId, teamId, sprintId);

            if (team == null)
            {
                return NotFound($"Error fetching team with ID: {teamId}");
            }

            return Ok(team);
        }
    }
}
