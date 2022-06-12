using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext, ILogger<UserRepository> logger) : base(dbContext, logger)
        {
        }

        public IQueryable<User> GetUserByEmail(string email)
        {
            return this.List(x => x.Email == email);
        }

        public IQueryable<User> GetUserByCode(string code)
        {
            return this.List(x => x.Code == code);
        }


    }
}
