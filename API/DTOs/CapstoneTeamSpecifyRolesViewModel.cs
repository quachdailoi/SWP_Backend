namespace API.DTOs
{
    public class CapstoneTeamSpecifyRolesViewModel
    {
        public int TeamId { get; set; }

        public UserCapstoneTeamViewModel Leader { get; set; } = new();

        public List<UserCapstoneTeamViewModel> Members { get; set; } = new();

        public List<UserCapstoneTeamViewModel> Mentors { get; set; } = new();

        public TopicCapstoneTeamViewModel Topic { get; set; } = new();

        public SemesterCapstoneTeamViewModel Semester { get; set; } = new();
    }
}
