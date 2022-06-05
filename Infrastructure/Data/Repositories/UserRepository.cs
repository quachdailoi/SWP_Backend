using Microsoft.Extensions.Logging;
using SECapstoneEvaluation.Domain.Entities;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;

namespace SECapstoneEvaluation.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext, ILogger<UserRepository> logger) : base(dbContext, logger)
        {
        }

        public IQueryable<User> GetUserByEmail(string email)
        {
            return this.DbSet.Where(x => x.Email == email);
        }
    }
}
