using Domain.Entities;
using Domain.Base;

namespace Domain.Entities
{
    public class RoleUser : BaseEntity
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int? ExaminationCouncilId { get; set; }
        public int? CapstoneTeamId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
        public CapstoneTeam? CapstoneTeam { get; set; }
    }
}
