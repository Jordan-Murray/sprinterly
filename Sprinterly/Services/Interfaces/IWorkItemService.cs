using Sprinterly.Models.WorkItems;

namespace Sprinterly.Services.Interfaces
{
    public interface IWorkItemService
    {
        public Task<IEnumerable<WorkItem>> FetchWorkItemsAsync(string organization, string projectId, string teamId,
            string sprintId, IEnumerable<string> areaPaths);
    }
}
