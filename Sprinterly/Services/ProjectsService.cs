using AutoMapper;
using Sprinterly.Models;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IDevOpsService _devOpsService;
        private readonly IMapper _mapper;

        public ProjectsService(IDevOpsService devOpsService, 
            IMapper mapper)
        {
            _devOpsService = devOpsService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Project>> GetProjects(string organization)
        {
            var url = $"{organization}/_apis/projects?api-version=7.0";

            var projectsResult = await _devOpsService.MakeDevOpsCall<DevOpsDTO<ProjectDTO>>(url);
            if(projectsResult != null)
            {
                var projects = _mapper.Map<IEnumerable<Project>>(projectsResult.Value);
                //var projects = projectsResult.Value.Map<IEnumerable<Project>>();
                return projects;
            }
            else
            {
                return Enumerable.Empty<Project>();
            }
        }

        public async Task<string> GetProjectNameById(string organization, string projectId)
        {
            var projects = await GetProjects(organization);
            return projects.Where(x => x.Id == projectId).FirstOrDefault()?.Name ?? string.Empty;
        }
    }
}
