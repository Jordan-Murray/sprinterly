using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        IDeveloperService _developerService;
        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<string>>> GetSprintNames([FromRoute] string organization, [FromRoute] string project)
        //{
        //    var developers = await _developerService.FetchDevelope

        //    if (sprints == null)
        //    {
        //        return NotFound("Error fetching sprints.");
        //    }

        //    return Ok(sprints.Select(x => x.Name));
        //}
    }
}
