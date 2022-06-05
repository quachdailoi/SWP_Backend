using Microsoft.Extensions.Logging;
using SECapstoneEvaluation.Domain.Entities;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;

namespace SECapstoneEvaluation.Infrastructure.Data.Repositories
{
    public class CampusRepository : GenericRepository<Campus>, ICampusRepository
    {
        public CampusRepository(AppDbContext dbContext, ILogger<GenericRepository<Campus>> logger) : base(dbContext, logger)
        {
        }
    }
}
