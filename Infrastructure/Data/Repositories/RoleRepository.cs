using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext, ILogger<RoleRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<Role?> GetRoleByName(string roleName)
        {
            return await List(x => x.Name.ToLower() == roleName.ToLower()).FirstOrDefaultAsync();
        }
    }
}
