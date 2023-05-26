using Sprinterly.Models.Teams;
using Sprinterly.Models;
using Sprinterly.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Sprinterly.Services
{
    public class TeamsService : ITeamsService
    {
        private ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private readonly string _apiVersion;
        private readonly IConfiguration _configuration;

        public TeamsService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<TeamsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();

            _configuration = configuration;
            var patToken = _configuration["AzureDevOps_PATToken"];
            _apiVersion = "7.0";

            _httpClient.BaseAddress = new Uri($"https://dev.azure.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{patToken}")));
            _logger = logger;
        }

        public async Task<IEnumerable<Team>> FetchTeamsAsync(string organization, string project)
        {
            var url = $"{organization}/_apis/projects/{project}/teams?api-version={_apiVersion}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var teams = JsonSerializer.Deserialize<DevOpsDTO<Team>>(json);
                    return teams.Value;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error fetching teams. PAT Token: {_configuration["AzureDevOps_PATToken"]}");
                throw;
            }

            return Enumerable.Empty<Team>();
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

        public async Task<Team?> GetTeamAsync(string organization, string project, string teamId)
        {
            var url = $"{organization}/{project}/_apis/teams/{teamId}?api-version={_apiVersion}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var team = JsonSerializer.Deserialize<Team>(json);

                return team;
            }

            return null;
        }
    }
}
