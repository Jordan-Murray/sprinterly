﻿using Sprinterly.Models.WorkItems;
using Sprinterly.Services.Interfaces;

namespace Sprinterly.Services
{
    public class SprintStatsService : ISprintStatsService
    {
        public float CalculateVelocity(List<WorkItemDTO> workItemDetails)
        {
            return workItemDetails.Sum(x => x.Fields.StoryPoints);
        }

        public int GetNumberOfBugs(List<WorkItemDTO> workItemDetails)
        {
            return workItemDetails.Count(w => w.Fields.WorkItemType == "Bug");
        }

        public int GetNumberOfUserStories(List<WorkItemDTO> workItemDetails)
        {
            return workItemDetails.Count(w => w.Fields.WorkItemType == "User Story");
        }

        public int GetNumberOfIssues(List<WorkItemDTO> workItemDetails)
        {
            return workItemDetails.Count(w => w.Fields.WorkItemType == "Issue");
        }
    }
}
