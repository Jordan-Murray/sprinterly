using Mapster;
using Sprinterly.Models;
using Sprinterly.Models.WorkItems;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IDevOpsService _devOpsService;
        private readonly IProjectsService _projectsService;
        private readonly ISprintService _sprintService;

        public WorkItemService(IDevOpsService devOpsService,
            IProjectsService projectsService,
            ISprintService sprintService)
        {
            _devOpsService = devOpsService;
            _projectsService = projectsService;
            _sprintService = sprintService;
        }
        public async Task<IEnumerable<WorkItem>> FetchWorkItemsAsync(string organization, string projectId, string teamId,
            string sprintId, IEnumerable<string> areaPaths)
        {
            var url = $"{organization}/{projectId}/_apis/wit/wiql?api-version=7.0";

            //project name comes through as ""
            var projectName = await _projectsService.GetProjectNameById(organization,projectId);
            var sprints = await _sprintService.GetSprintsForTeam(organization, projectId, teamId);
            var selectedSprint = sprints.Where(x => x.Id == sprintId).First();

            var areaPathFilter = string.Join(", ", areaPaths.Select(path => $"'{path}'"));

            var query = $@"
                SELECT [System.Id]
                FROM WorkItems
                WHERE [System.TeamProject] = @project
                AND [System.AreaPath] IN ({areaPathFilter})
                AND [System.WorkItemType] IN ('User Story', 'Bug', 'Issue')
                AND [System.State] = 'Closed'
                AND [System.IterationPath] = '{projectName}\\{selectedSprint.Name}'";

            var response = await _devOpsService.MakeDevOpsQuery<WorkItemListDTO>(url, query);

            var workItems = await FetchWorkItemDetailsAsync(organization, projectId, response.Value.Select(wi => wi.Id));
            return workItems;
        }

        public async Task<List<WorkItem>> FetchWorkItemDetailsAsync(string organization, string projectId, IEnumerable<int> workItemIds)
        {
            if (workItemIds.Count() == 0)
            {
                return new List<WorkItem>();
            }

            string ids = string.Join(",", workItemIds);
            string url = $"{organization}/{projectId}/_apis/wit/workitems?ids={ids}&api-version=7.0";

            var workItemsResponse = await _devOpsService.MakeDevOpsCall<DevOpsDTO<WorkItemDTO>>(url);
            //SWITCH TO AUTO MAPPER - https://chat.openai.com/share/ab0b5608-a50f-41d2-a796-d60a9bfe04ec
            var workItems = workItemsResponse.Value.Adapt<List<WorkItem>>();
            return workItems;
        }
    }
}
