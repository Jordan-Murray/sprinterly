using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/{project}/[controller]")]
    [ApiController]
    public class SprintStatsController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;
        private readonly ISprintStatsService _sprintStatsService;

        public SprintStatsController(IDevOpsService devOpsService, ISprintStatsService sprintStatsService)
        {
            _devOpsService = devOpsService;
            _sprintStatsService = sprintStatsService;
        }

        //[HttpGet("velocity")]
        //public async Task<ActionResult<IEnumerable<string>>> GetVelocityInSprint(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
        //    var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, project, workItems);
        //    var velocity = _sprintStatsService.CalculateVelocity(workItemDetails);

        //    return Ok(velocity);
        //}

        //[HttpGet("bugs")]
        //public async Task<ActionResult<IEnumerable<string>>> GetBugsCompletedInSprint(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
        //    var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, project, workItems);
        //    var bugsCompleted = _sprintStatsService.GetNumberOfBugs(workItemDetails);

        //    return Ok(bugsCompleted);
        //}

        //[HttpGet("userstories")]
        //public async Task<ActionResult<IEnumerable<string>>> GetUserStoriesCompletedInSprint(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
        //    var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, project, workItems);
        //    var completedUserStories = _sprintStatsService.GetNumberOfUserStories(workItemDetails);

        //    return Ok(completedUserStories);
        //}

        //[HttpGet("issues")]
        //public async Task<ActionResult<IEnumerable<string>>> GetIssuesCompletedInSprint(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
        //    var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, project, workItems);
        //    var completedIssues = _sprintStatsService.GetNumberOfIssues(workItemDetails);

        //    return Ok(completedIssues);
        //}

        //[HttpGet("time")]
        //public async Task<ActionResult<IEnumerable<string>>> GetHoursCompletedOnUserStory(
        //    [FromRoute] string organization, [FromRoute] string project, [FromQuery] string sprint, [FromQuery] string team)
        //{
        //    var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
        //    var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
        //    var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, project, workItems);
        //    foreach (var workItemDetail in workItemDetails)
        //    {
        //        var hoursSpent = await _devOpsService.GetHoursSpentOnWorkItem(organization, project, workItemDetail.Id);
        //        workItemDetail.HoursSpent = hoursSpent;
        //    }

        //    return Ok(workItemDetails);
        //}
    }
}