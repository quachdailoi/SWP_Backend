using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class CampusRepository : GenericRepository<Campus>, ICampusRepository
    {
        public CampusRepository(AppDbContext dbContext, ILogger<GenericRepository<Campus>> logger) : base(dbContext, logger)
        {
        }
    }
}
