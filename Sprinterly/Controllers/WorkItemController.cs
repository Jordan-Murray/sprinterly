using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{project}/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;

        public WorkItemController(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<string>>> GetWorkItemsInSprint(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);

        //    if (workItems == null)
        //    {
        //        return NotFound("Error fetching work items.");
        //    }

        //    return Ok(workItems);
        //}

        //[HttpGet("details")]
        //public async Task<ActionResult<IEnumerable<string>>> GetWorkDetailsInSprint(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
        //    var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, project, workItems);

        //    if (workItemDetails == null)
        //    {
        //        return NotFound("Error fetching work item details.");
        //    }

        //    return Ok(workItemDetails);
        //}

        //[HttpGet("tasks")]
        //public async Task<ActionResult<IEnumerable<string>>> GetTasksForSprint(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
        //    var tasks = await _devOpsService.FetchChildTasksForWorkItems(organization, project, workItems);
        //    //var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, project, workItems);

        //    if (workItemDetails == null)
        //    {
        //        return NotFound("Error fetching work item details.");
        //    }

        //    return Ok(workItemDetails);
        //}
    }
}
