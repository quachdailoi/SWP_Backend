using Microsoft.Extensions.Logging;
using SECapstoneEvaluation.Domain.Entities;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;

namespace SECapstoneEvaluation.Infrastructure.Data.Repositories
{
    public class RoleUserRepository : GenericRepository<RoleUser>, IRoleUserRepository
    {
        public RoleUserRepository(AppDbContext dbContext, ILogger<RoleUserRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
