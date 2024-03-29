﻿using Sprinterly.Models.Sprints;
using Sprinterly.Models;
using Sprinterly.Services.Interfaces;
using Sprinterly.Models.Teams;
using AutoMapper;

namespace Sprinterly.Services
{
    public class SprintService : ISprintService
    {
        private readonly IDevOpsService _devOpsService;
        private readonly IMapper _mapper;

        public SprintService(IDevOpsService devOpsService, 
            IMapper mapper)
        {
            _devOpsService = devOpsService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Sprint>> GetSprintsForTeam(string organization, string projectId, string teamId)
        {
            var sprintsToReturn = new List<Sprint>();
            var url = $"{organization}/{projectId}/{teamId}/_apis/work/teamsettings/iterations?api-version=7.0";

            var sprintsResult = await _devOpsService.MakeDevOpsCall<DevOpsDTO<SprintDTO>>(url);
            if (sprintsResult != null)
            {
                foreach (var sprintDTO in sprintsResult.Value)
                {
                    sprintsToReturn.Add(_mapper.Map<Sprint>(sprintDTO));
                    //sprintsToReturn.Add(sprintDTO.Map<Sprint>());
                }
                return sprintsToReturn;
            }
            else
            {
                return Enumerable.Empty<Sprint>();
            }
        }
    }
}
