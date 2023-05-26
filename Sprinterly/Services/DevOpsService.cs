using Sprinterly.Models;
using Sprinterly.Models.Sprints;
using Sprinterly.Models.Teams;
using Sprinterly.Models.WorkItems;
using Sprinterly.Services.Interfaces;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Sprinterly.Services
{
    public class DevOpsService : IDevOpsService
    {
        private ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private readonly string _apiVersion;
        private readonly IConfiguration _configuration;

        public DevOpsService(IHttpClientFactory httpClientFactory, 
            IConfiguration configuration, 
            ILogger<DevOpsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            _configuration = configuration;

            var patToken = _configuration["AzureDevOps_PATToken"];
            //var patToken = Environment.GetEnvironmentVariable("AzureDevOps_PATToken");
            _apiVersion = "7.0";

            _httpClient.BaseAddress = new Uri($"https://dev.azure.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{patToken}")));
            _logger = logger;
        }

        public async Task<IEnumerable<Sprint>> FetchSprintsAsync(string organization, string project)
        {
            var pageSize = 100;
            var skip = 0;

            var url = $"{organization}/{project}/_apis/work/teamsettings/iterations?api-version={_apiVersion}&$skip={skip}&$top={pageSize}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var sprints = JsonSerializer.Deserialize<DevOpsDTO<Sprint>>(json);
                return sprints.Value;
            }

            return Enumerable.Empty<Sprint>();
        }


        public async Task<Sprint?> FetchSprintByNameAsync(string organization, string project, string name)
        {
            var sprints = await FetchSprintsAsync(organization,project);
            return sprints.Where(x => x.Name == name).FirstOrDefault();
        }




        public async Task<IEnumerable<int>> FetchWorkItemsAsync(string organization, string project, string sprintName, IEnumerable<string> areaPaths)
        {
            var url = $"{organization}/{project}/_apis/wit/wiql?api-version={_apiVersion}";

            var areaPathFilter = string.Join(", ", areaPaths.Select(path => $"'{path}'"));

            var query = $@"
                SELECT [System.Id]
                FROM WorkItems
                WHERE [System.TeamProject] = @project
                AND [System.AreaPath] IN ({areaPathFilter})
                AND [System.WorkItemType] IN ('User Story', 'Bug', 'Issue')
                AND [System.State] = 'Closed'
                AND [System.IterationPath] = '{project}\\{sprintName}'";

            var response = await _httpClient.PostAsJsonAsync(url, new { query });

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var workItemsResponse = JsonSerializer.Deserialize<WorkItemListDTO>(json);
                return workItemsResponse.Value.Select(wi => wi.Id);
            }

            return Enumerable.Empty<int>();
        }

        public async Task<List<WorkItemDTO>> FetchWorkItemDetailsAsync(string organization, string project, IEnumerable<int> workItemIds)
        {
            if (workItemIds.Count() == 0)
            {
                return new List<WorkItemDTO>();
            }

            string ids = string.Join(",", workItemIds);
            string url =    $"{organization}/{project}/_apis/wit/workitems?ids={ids}&api-version={_apiVersion}";
            var secondUrl = $"{organization}/{project}/_apis/wit/workitems?ids={ids}&api-version={_apiVersion}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var workItemResponse = JsonSerializer.Deserialize<DevOpsDTO<WorkItemDTO>>(json);
                    return workItemResponse.Value;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<WorkItemDTO?> FetchWorkItemAsync(string organization, string project, int workItemId)
        {
            var url = $"{organization}/{project}/_apis/wit/workItems/{workItemId}?api-version={_apiVersion}&$expand=Relations";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var workItem = JsonSerializer.Deserialize<WorkItemDTO>(content);
                    return workItem;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public int ExtractIdFromUrl(string url)
        {
            var match = Regex.Match(url, @"/workItems/(\d+)");
            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value);
            }
            else
            {
                throw new Exception("Failed to extract ID from URL");
            }
        }

        public async Task<float> GetHoursSpentOnWorkItem(string organization, string project, int workItemId)
        {
            var workItem = await FetchWorkItemAsync(organization, project, workItemId);

            var hoursSpent = 0f;
            if (workItem.Relations != null)
            {
                foreach (var relation in workItem.Relations)
                {
                    if (relation.RelationshipType == "System.LinkTypes.Hierarchy-Forward")
                    {
                        var childId = ExtractIdFromUrl(relation.Url);
                        var childTask = await FetchWorkItemAsync(organization, project, childId);
                        hoursSpent += childTask.Fields.CompletedHours;
                    }
                }
            }
            return hoursSpent;
        }
    }
}
