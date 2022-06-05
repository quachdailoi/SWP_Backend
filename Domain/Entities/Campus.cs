using SECapstoneEvaluation.Domain.Base;

namespace SECapstoneEvaluation.Domain.Entities
{
    public class Campus : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; }
    }
}
