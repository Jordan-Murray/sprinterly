﻿using Sprinterly.Models.Teams;
using Sprinterly.Models;
using Sprinterly.Services.Interfaces;
using AutoMapper;

namespace Sprinterly.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly IDevOpsService _devOpsService;
        private readonly ISprintService _sprintService;
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;

        public TeamsService(IDevOpsService devOpsService, 
            ISprintService sprintService, 
            IWorkItemService workItemService, 
            IMapper mapper)
        {
            _devOpsService = devOpsService;
            _sprintService = sprintService;
            _workItemService = workItemService;
            _mapper = mapper;
        }

        public async Task<List<TeamMember>> PopulateTeamMembersWithStats(string organization, string projectId, string teamId,
            List<TeamMember> teamMembers, string sprintId)
        {
            var areaPaths = await FetchAreaPathsForTeam(organization, projectId, teamId);
            var workItems = await _workItemService.FetchWorkItemsAsync(organization, projectId, teamId, sprintId, areaPaths);
            foreach (var teamMember in teamMembers)
            {
                var memberWorkItems = workItems.Where(w => w.AssignedTo == teamMember.DisplayName).ToList();

                teamMember.UserStoriesCompleted = memberWorkItems.Count(w => w.Type == "User Story" && w.State == "Closed");
                teamMember.BugsCompleted = memberWorkItems.Count(w => w.Type == "Bug" && w.State == "Closed");
                teamMember.IssuesCompleted = memberWorkItems.Count(w => w.Type == "Issue" && w.State == "Closed");
                teamMember.Velocity = (int)memberWorkItems.Where(w => w.State == "Closed").Sum(w => w.StoryPoints);
            }
            return teamMembers;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync(string organization, string projectId)
        {
            var url = $"{organization}/_apis/projects/{projectId}/teams?api-version=7.0";

            var teamsWithMembers = new List<Team>();

            var teamsResult = await _devOpsService.MakeDevOpsCall<DevOpsDTO<TeamDTO>>(url);
            if (teamsResult != null)
            {
                var teams = _mapper.Map<IEnumerable<Team>>(teamsResult.Value);
                //var teams = teamsResult.Value.Map<IEnumerable<Team>>();
                foreach (var team in teams)
                {
                    var teamMembers = await GetMembersForTeam(organization, projectId, team.Id);
                    team.NumberOfMembers = teamMembers.Count;
                    teamsWithMembers.Add(team);
                }
                return teamsWithMembers;
            }
            else
            {
                return Enumerable.Empty<Team>();
            }
        }

        public async Task<Team?> GetTeamAsync(string organization, string projectId, string teamId, string sprintId)
        {
            var url = $"{organization}/_apis/projects/{projectId}/teams/{teamId}?api-version=7.0";

            var teamResult = await _devOpsService.MakeDevOpsCall<TeamDTO>(url);
            if (teamResult == null)
            {
                return null;
            }
            
            var team = _mapper.Map<Team>(teamResult);
            //var team = teamResult.Map<Team>();
            var teamMembers = await GetMembersForTeam(organization, projectId, teamId);
            team.TeamMembers = teamMembers;
            var teamMembersWithStats = await PopulateTeamMembersWithStats(organization, projectId, teamId, teamMembers, sprintId);
            team.NumberOfMembers = teamMembers.Count;
            return team;
        }

        private async Task<List<TeamMember>> GetMembersForTeam(string organization, string projectId ,string teamId)
        {
            var url = $"{organization}/_apis/projects/{projectId}/teams/{teamId}/members?api-version=7.0";
            
            var teamMembers = new List<TeamMember>();

            var teamMembersResult = await _devOpsService.MakeDevOpsCall<DevOpsDTO<TeamMemberDTO>>(url);
            if (teamMembersResult != null)
            {
                foreach (var teamMemberDTO in teamMembersResult.Value)
                {
                    var teamMember = _mapper.Map<TeamMember>(teamMemberDTO.Details);
                    //var teamMember = teamMemberDTO.Details.Map<TeamMember>();
                    teamMembers.Add(teamMember);
                }
            }

            return teamMembers;
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

        public async Task<IEnumerable<string>> FetchAreaPathsForTeam(string organization, string projectId, string teamId)
        {
            var url = $"{organization}/{projectId}/{teamId}/_apis/work/teamsettings/teamfieldvalues?api-version=7.0";

            var areaPathsResult = await _devOpsService.MakeDevOpsCall<DevOpsValuesDTO<TeamFieldValue>>(url);

                //var allAreaPaths = await FetchAreaPathsAsync(organization, projectId);
                //var teamAreaPaths = new List<string>();

                //foreach (var value in areaPathsResult.Value)
                //{
                //    var matchingAreaPaths = allAreaPaths.Where(path => path.Contains(value.Value));
                //    teamAreaPaths.AddRange(matchingAreaPaths);
                //}

                //return teamAreaPaths;
            if (areaPathsResult == null)
            {
                return Enumerable.Empty<string>();
            }
            var areaPaths = new List<string>();
            areaPathsResult.Value.ForEach(x =>
            {
                areaPaths.Add(x.Value.ToString());
            });
            return areaPaths;
        }

        public async Task<IEnumerable<string>> FetchAreaPathsAsync(string organization, string project)
        {
            var url = $"{organization}/{project}/_apis/wit/classificationnodes?api-version=7.0&$depth=10&$expand=all";
            var areaPathsResult = await _devOpsService.MakeDevOpsCall<DevOpsDTO<AreaPathNode>>(url);

            if (areaPathsResult != null)
            {
                var areaPaths = new List<string>();
                ExtractAreaPaths(areaPathsResult.Value[0], areaPaths);
                areaPaths = areaPaths.Select(x => x.Replace(@"\Area", "")).ToList();
                areaPaths = areaPaths.Select(x => x.Substring(1)).ToList();
                return areaPaths;
            }

            return Enumerable.Empty<string>();
        }



        //public async Task<Team?> GetTeamAsync(string organization, string project, string teamId)
        //{
        //    var url = $"{organization}/{project}/_apis/teams/{teamId}?api-version={_apiVersion}";
        //    var response = await _httpClient.GetAsync(url);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var team = JsonSerializer.Deserialize<Team>(json);

        //        return team;
        //    }

        //    return null;
        //}
    }
}
