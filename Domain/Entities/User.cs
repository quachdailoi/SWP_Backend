using Domain.Base;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public int CampusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool Status { get; set; }

        public Campus Campus { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
    }
}
