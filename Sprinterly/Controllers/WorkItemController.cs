﻿using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IDevOpsService _devOpsService;

        public WorkItemController(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }

        [HttpGet] //ToDo change sprints and teams so can be past in as body
        public async Task<ActionResult<IEnumerable<string>>> GetWorkItemsInSprint(string organization, string project, string sprint, string  team)
        {
            var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
            var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);

            if (workItems == null)
            {
                return NotFound("Error fetching work items.");
            }

            return Ok(workItems);
        }

        [HttpGet] //ToDo change sprints and teams so can be past in as body
        public async Task<ActionResult<IEnumerable<string>>> GetWorkDetailsInSprint(string organization, string project, string sprint, string team)
        {
            var areaPaths = await _devOpsService.FetchAreaPathsForTeam(organization, project, team);
            var workItems = await _devOpsService.FetchWorkItemsAsync(organization, project, sprint, areaPaths);
            var workItemDetails = await _devOpsService.FetchWorkItemDetailsAsync(organization, sprint, workItems);

            if (workItemDetails == null)
            {
                return NotFound("Error fetching work item details.");
            }

            return Ok(workItems);
        }
    }
}
