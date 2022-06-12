namespace API.DTOs
{
    public class ExcelCreateCapstoneTeamViewModel
    {
        public string LeaderCode { get; set; }
        public string? Member1Code { get; set; }
        public string? Member2Code { get; set; }
        public string? Member3Code { get; set; } 
        public string? Member4Code { get; set; }

        public string Mentor1Code { get; set; }
        public string? Mentor2Code { get; set; }

        public string TopicCode { get; set; }
        
        public string SemesterCode { get; set; }

        public int TopicId { get; set; }

        public int SemesterId { get; set; }
    }
}
