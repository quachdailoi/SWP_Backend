﻿namespace SECapstoneEvaluation.APIs.DTOs
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public int CampusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
