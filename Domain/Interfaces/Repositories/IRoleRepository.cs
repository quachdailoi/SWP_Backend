﻿using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetRoleByName(string roleName);
    }
}
