using SECapstoneEvaluation.Domain.Base;

namespace SECapstoneEvaluation.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<RoleUser> RoleUsers { get; set; }
    }
}
