using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprinterly.Models.Teams;
using Sprinterly.Services;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/{organization}/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams([FromRoute] string organization)
        {
            var projects = await _projectsService.GetProjects(organization);

            return Ok(projects);
        }
    }
}
