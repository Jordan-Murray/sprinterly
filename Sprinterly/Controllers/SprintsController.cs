using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintsController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;

        public SprintsController(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetSprintNames()
        {
            var sprints = await _devOpsService.FetchSprintsAsync();

            if (sprints == null)
            {
                return NotFound("Error fetching sprints.");
            }

            return Ok(sprints.Select(x => x.Name));
        }
    }
}
