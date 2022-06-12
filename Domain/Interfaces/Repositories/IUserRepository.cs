using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IQueryable<User> GetUserByEmail(string email);

        IQueryable<User> GetUserByCode(string code);
    }
}
