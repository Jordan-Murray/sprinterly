namespace Sprinterly.Services.Interfaces
{
    public interface IDevOpsService
    {
        public Task<IEnumerable<string>> FetchTeamNamesAsync();
    }
}
