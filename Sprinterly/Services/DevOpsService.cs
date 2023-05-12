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

            _organization = _configuration["AzureDevOps:Organization"];
            _project = _configuration["AzureDevOps:Project"];
            var patToken = _configuration["AzureDevOps:PATToken"];
            _apiVersion = "7.0";

            _httpClient.BaseAddress = new Uri($"https://dev.azure.com/{_organization}/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{patToken}")));
        }


        public async Task<IEnumerable<string>> FetchTeamNamesAsync()
        {
            var url = $"_apis/projects/{_project}/teams?api-version={_apiVersion}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var teams = JsonSerializer.Deserialize<DevOpsResponse<Team>>(json);
                return teams.Value.Select(team => team.Name);
            }

            return null;
        }

        public async Task<List<Sprint>> FetchSprintsAsync()
        {
            var pageSize = 100;
            var skip = 0;

            var url = $"{_project}/_apis/work/teamsettings/iterations?api-version={_apiVersion}&$skip={skip}&$top={pageSize}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var sprints = JsonSerializer.Deserialize<DevOpsResponse<Sprint>>(json);
                return sprints.Value;
            }

            return null;
        }


        public async Task<Sprint> FetchSprintByNameAsync(string name)
        {
            var sprints = await FetchSprintsAsync();
            return sprints.First();
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
