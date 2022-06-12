using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class RoleUserRepository : GenericRepository<RoleUser>, IRoleUserRepository
    {
        public RoleUserRepository(AppDbContext dbContext, ILogger<RoleUserRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
