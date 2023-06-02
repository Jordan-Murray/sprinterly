using Sprinterly.Models;
using Sprinterly.Models.WorkItems;
using Sprinterly.Services.Interfaces;
using System.Net.Http;

namespace Sprinterly.Services
{
    public class WorkItemService
    {
        private readonly IDevOpsService _devOpsService;
        private readonly IProjectsService _projectsService;

        public WorkItemService(IDevOpsService devOpsService, 
            IProjectsService projectsService)
        {
            _devOpsService = devOpsService;
            _projectsService = projectsService;
        }
        public async Task<IEnumerable<WorkItem>> FetchWorkItemsAsync(string organization, string projectId, string sprintId, IEnumerable<string> areaPaths)
        {
            var url = $"{organization}/{projectId}/_apis/wit/wiql?api-version=7.0";

            var projectName = await _projectsService.GetProjectNameById(organization,projectId);
            var sprintName = await GetSprintName(organization, projectId, sprintId);

            var areaPathFilter = string.Join(", ", areaPaths.Select(path => $"'{path}'"));

            var query = $@"
                SELECT [System.Id]
                FROM WorkItems
                WHERE [System.TeamProject] = @project
                AND [System.AreaPath] IN ({areaPathFilter})
                AND [System.WorkItemType] IN ('User Story', 'Bug', 'Issue')
                AND [System.State] = 'Closed'
                AND [System.IterationPath] = '{projectName}\\{sprintName}'";

            var response = await _devOpsService.MakeDevOpsQuery<WorkItemListDTO>(url, query);

            var workItems = await FetchWorkItemDetailsAsync(organization, projectId, response.Value.Select(wi => wi.Id))
        }

        public async Task<List<WorkItemDTO>> FetchWorkItemDetailsAsync(string organization, string projectId, IEnumerable<int> workItemIds)
        {
            if (workItemIds.Count() == 0)
            {
                return new List<WorkItemDTO>();
            }

            string ids = string.Join(",", workItemIds);
            string url = $"{organization}/{projectId}/_apis/wit/workitems?ids={ids}&api-version=7.0";

            var workItemsResponse = _devOpsService.MakeDevOpsCall<DevOpsDTO<WorkItemDTO>>(url);

            
        }
    }
}
