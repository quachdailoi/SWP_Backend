namespace API.DTOs
{
    public class CapstoneTeamViewModel
    {
        public int TeamId { get; set; }

        public List<UserCapstoneTeamViewModel> Users { get; set; } = new();

        public TopicCapstoneTeamViewModel Topic { get; set; } = new();

        public SemesterCapstoneTeamViewModel Semester { get; set; } = new();
    }
}
