namespace Sprinterly.Models.Teams
{
    public class TeamMember
    {
        public string DisplayName { get; set; } = string.Empty;
        public int UserStoriesCompleted { get; set; }
        public int BugsCompleted { get; set; }
        
        public int IssuesCompleted { get; set; }
        public int Velocity { get; set; }
    }
}
