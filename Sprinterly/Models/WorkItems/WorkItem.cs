namespace Sprinterly.Models.WorkItems
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AssignedTo { get; set; }
        public string Type { get; set; }
        public string IterationPath { get; set; }
        public string State { get; set; }
        public string AreaPath { get; set; }
        public float StoryPoints { get; set; }

        public WorkItem()
        {
                 
        }
    }
}
