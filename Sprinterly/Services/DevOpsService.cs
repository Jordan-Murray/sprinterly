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
                var teams = JsonSerializer.Deserialize<TeamResponse>(json);
                return teams.Teams.Select(team => team.Name);
            }

            return null;
        }

    }
}
