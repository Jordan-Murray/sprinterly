using Mapster;
using Sprinterly.Models;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IDevOpsService _devOpsService;

        public ProjectsService(IDevOpsService devOpsService)
        {
            _devOpsService = devOpsService;
        }
        public async Task<IEnumerable<Project>> GetProjects(string organization)
        {
            var url = $"{organization}/_apis/projects?api-version=7.0";

            var projectsResult = await _devOpsService.MakeDevOpsCall<DevOpsDTO<ProjectDTO>>(url);
            if(projectsResult != null)
            {
                var projects = projectsResult.Value.Adapt<IEnumerable<Project>>();
                return projects;
            }
            else
            {
                return Enumerable.Empty<Project>();
            }
        }

        public async Task<string> GetProjectNameById(string organization, string projectId)
        {
            return string.Empty;
        }
    }
}
