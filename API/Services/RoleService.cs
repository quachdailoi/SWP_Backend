using API.Services.Constracts;
using Domain.Interfaces.Repositories;

namespace API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public async Task<int> GetRoleIdByName(string roleName)
        {
            var role = await roleRepository.GetRoleByName(roleName);
            if (role == null)
            {
                throw new Exception($"Not found role with name: {roleName}");
            }

            return role.Id;
        }
    }
}
