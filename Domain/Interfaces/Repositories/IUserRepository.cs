using SECapstoneEvaluation.Domain.Entities;

namespace SECapstoneEvaluation.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IQueryable<User> GetUserByEmail(string email);
    }
}
