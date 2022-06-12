namespace API.DTOs
{
    public class UserRolesDto
    {
        public int Id { get; set; }
        public int CampusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public List<RoleDto> Roles { get; set; }
    }
}
