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
        public async Task<ActionResult<IEnumerable<string>>> GetTeams()
        {
            var teams = await _devOpsService.FetchTeamNamesAsync();

            if (teams == null)
            {
                return NotFound("Error fetching teams.");
            }

            return Ok(teams);
        }
    }
}
