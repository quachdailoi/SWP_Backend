using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CapstoneTeam : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public int Status { get; set; }
        public int SemesterId { get; set; }
        public int TopicId { get; set; }

        public Semester Semester { get; set; } = new();
        public Topic? Topic { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
    }
}
