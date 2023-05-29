using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{project}/[controller]")]
    [ApiController]
    public class OldSprintsController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;

        public OldSprintsController(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetSprintNames([FromRoute] string organization, [FromRoute] string project)
        {
            var sprints = await _devOpsService.FetchSprintsAsync(organization, project);

            if (sprints == null)
            {
                return NotFound("Error fetching sprints.");
            }

            return Ok(sprints.Select(x => x.Name));
        }
    }
}
