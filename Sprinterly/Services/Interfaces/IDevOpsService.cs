﻿using Sprinterly.Models;
using Sprinterly.Models.Sprints;
using Sprinterly.Models.WorkItems;
using System;

namespace Sprinterly.Services.Interfaces
{
    public interface IDevOpsService
    {
        public Task<T> MakeDevOpsCall<T>(string url);
        //public Task<IEnumerable<Sprint>> FetchSprintsAsync(string organization, string project);
        //Task<IEnumerable<int>> FetchWorkItemsAsync(string organization, string project, string sprintName, IEnumerable<string> areaPaths);
        //Task<List<WorkItemDTO>> FetchWorkItemDetailsAsync(string organization, string project, IEnumerable<int> workItemIds);
        //Task<float> GetHoursSpentOnWorkItem(string organization, string project, int workItemId);
    }
}
