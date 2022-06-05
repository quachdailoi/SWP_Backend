using Microsoft.Extensions.Logging;
using SECapstoneEvaluation.Domain.Entities;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;

namespace SECapstoneEvaluation.Infrastructure.Data.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext, ILogger<RoleRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
