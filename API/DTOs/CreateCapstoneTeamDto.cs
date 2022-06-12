namespace API.DTOs
{
    public class CreateCapstoneTeamDto
    {
        public int TeamId { get; set; }
        public int LeaderId { get; set; }

        public List<int> MemberIds { get; set; } = new();

        public List<int> MentorIds { get; set; } = new();

        public int TopicId { get; set; }

        public int SemesterId { get; set; }
    }
}
