using API.Services.Constracts;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class RoleUserService : IRoleUserService
    {
        private readonly IRoleUserRepository roleUserRepository;

        public RoleUserService(IRoleUserRepository roleUserRepository)
        {
            this.roleUserRepository = roleUserRepository;
        }

        public async Task<IEnumerable<RoleUser>> GetAll()
        {
            return await roleUserRepository.List().ToListAsync();
        }

        public async Task<RoleUser> CreateRoleUser(RoleUser roleUser)
        {
            return await roleUserRepository.Add(roleUser);
        }
    }
}
