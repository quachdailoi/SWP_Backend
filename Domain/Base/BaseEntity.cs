using Domain.Interfaces.Entities;

namespace Domain.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; } 
        public string? UpdatedBy { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}
