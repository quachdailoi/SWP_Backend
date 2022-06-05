using SECapstoneEvaluation.Domain.Base;

namespace SECapstoneEvaluation.Domain.Entities
{
    public class RoleUser : BaseEntity
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int? ExaminationCouncilId { get; set; }
        public int? CapstoneTeamId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
