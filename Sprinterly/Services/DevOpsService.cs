using Sprinterly.Models;
using Sprinterly.Services.Interfaces;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Sprinterly.Services
{
    public class DevOpsService : IDevOpsService
    {
        private ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private readonly string _organization;
        private readonly string _project;
        private readonly string _apiVersion;
        private readonly IConfiguration _configuration;

        public DevOpsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
        }


        public async Task<IEnumerable<string>> FetchTeamNamesAsync(string organization, string project)
        {
            var url = $"{organization}/_apis/projects/{project}/teams?api-version={_apiVersion}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var teams = JsonSerializer.Deserialize<DevOpsDTO<Team>>(json);
                    return teams.Value.Select(team => team.Name);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error fetching teams. PAT Token: {_configuration["AzureDevOps_PATToken"]}");
                throw;
            }


            return Enumerable.Empty<string>();
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

        public async Task<IEnumerable<string>> FetchAreaPathsAsync(string organization, string project)
        {
            var url = $"{organization}/{project}/_apis/wit/classificationnodes?api-version={_apiVersion}&$depth=10&$expand=all";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var areaPathsResponse = JsonSerializer.Deserialize<DevOpsDTO<AreaPathNode>>(json);

                var areaPaths = new List<string>();
                ExtractAreaPaths(areaPathsResponse.Value[0], areaPaths);
                areaPaths = areaPaths.Select(x => x.Replace(@"\Area", "")).ToList();
                areaPaths = areaPaths.Select(x => x.Substring(1)).ToList();
                return areaPaths;
            }

            return Enumerable.Empty<string>();
        }

        private void ExtractAreaPaths(AreaPathNode node, List<string> areaPaths)
        {
            areaPaths.Add(node.Path);

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    ExtractAreaPaths(child, areaPaths);
                }
            }
        }

        public async Task<IEnumerable<string>> FetchAreaPathsForTeam(string organization, string project, string teamName)
        {
            var url = $"{organization}/{project}/{teamName}/_apis/work/teamsettings/teamfieldvalues?api-version={_apiVersion}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var teamFieldValuesResponse = JsonSerializer.Deserialize<DevOpsValuesDTO<TeamFieldValue>>(json);
                var allAreaPaths = await FetchAreaPathsAsync(organization, project);



                var teamAreaPaths = new List<string>();

                foreach (var value in teamFieldValuesResponse.Value)
                {
                    var matchingAreaPaths = allAreaPaths.Where(path => path.Contains(value.Value));
                    teamAreaPaths.AddRange(matchingAreaPaths);
                }

                return teamAreaPaths;
            }

            return Enumerable.Empty<string>();
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
                var workItemsResponse = JsonSerializer.Deserialize<WorkItemDTO>(json);
                return workItemsResponse.Value.Select(wi => wi.Id);
            }

            return Enumerable.Empty<int>();
        }

        public async Task<IEnumerable<WorkItemDetail>> FetchWorkItemDetailsAsync(IEnumerable<int> workItemIds, string organization, string project, string apiVersion)
        {
            if (!workItemIds.Any())
            {
                return Enumerable.Empty<WorkItemDetail>();
            }

            var ids = string.Join(",", workItemIds);
            var url = $"https://dev.azure.com/{organization}/{project}/_apis/wit/workitems?ids={ids}&api-version={apiVersion}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var workItemDetailsResponse = JsonSerializer.Deserialize<DevOpsDTO<WorkItemDetail>>(json);
                    return workItemDetailsResponse.Value;
                }
            }
            catch (Exception ex)
            {
                // Log your error message here
                Console.WriteLine("Error fetching work item details: " + ex.Message);
            }

            return Enumerable.Empty<WorkItemDetail>();
        }

        //public async Task<List<SprintStats>> AnalyzeSprintAsync(string sprintName, List<string> teamNames)
        //{
        //    var sprint = await FetchSprintByNameAsync(sprintName);
        //    var teamStatsPromises = teamNames.Select(async teamName =>
        //    {
        //        var areaPaths = await FetchTeamAreaPathsAsync(teamName);
        //        var workItemIds = await FetchWorkItemsAsync(sprint.Name, areaPaths);
        //        var workItemDetails = await FetchWorkItemDetailsAsync(workItemIds);
        //        var stats = await AnalyzeSprintAsync(sprint, workItemDetails, teamName);
        //        return stats;
        //    }).ToList();

        //    var teamStats = await Task.WhenAll(teamStatsPromises);
        //    return teamStats.ToList();
        //}

    }
}
