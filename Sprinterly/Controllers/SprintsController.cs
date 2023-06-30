using Microsoft.AspNetCore.Mvc;
using Sprinterly.Models.Sprints;
using Sprinterly.Models.Teams;
using Sprinterly.Services;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{projectId}/[controller]")]
    [ApiController]
    public class SprintsController : ControllerBase
    {
        private readonly ISprintService _sprintService;
        private readonly ITeamsService _teamsService;

        public SprintsController(ISprintService sprintService, ITeamsService teamsService)
        {
            _sprintService = sprintService;
            _teamsService = teamsService;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult<IEnumerable<Sprint>>> GetSprinsForTeam([FromRoute] string organization, [FromRoute] string projectId,
            [FromRoute] string teamId)
        {
            var sprints = await _sprintService.GetSprintsForTeam(organization, projectId, teamId);

            if (sprints == null)
            {
                return NotFound("Error fetching sprints.");
            }

            return Ok(sprints);
        }
    }
}
