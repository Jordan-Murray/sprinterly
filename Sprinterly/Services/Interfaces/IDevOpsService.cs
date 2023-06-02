using Sprinterly.Models;
using Sprinterly.Models.Sprints;
using Sprinterly.Models.WorkItems;
using System;

namespace Sprinterly.Services.Interfaces
{
    public interface IDevOpsService
    {
        public Task<T> MakeDevOpsCall<T>(string url);
        public Task<T?> MakeDevOpsQuery<T>(string url, string query);
    }
}
