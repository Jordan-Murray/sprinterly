using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintStatsController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;

        public SprintStatsController(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }

        //[HttpPost]
        //public async Task<ActionResult<List<SprintStats>>> AnalyzeSprint([FromBody] AnalyzeSprintRequest request)
        //{
        //    if (request == null || string.IsNullOrWhiteSpace(request.SprintName) || request.TeamNames == null)
        //    {
        //        return BadRequest("Invalid request");
        //    }

        //    var result = await _azureDevOpsService.AnalyzeSprintAsync(request.SprintName, request.TeamNames);

        //    if (result == null)
        //    {
        //        return NotFound("Error analyzing sprint");
        //    }

        //    return Ok(result);
        //}
    }
}
