using Domain.Interfaces.Repositories;
using Domain.Shared.Enums;

namespace Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleUserRepository RoleUser { get; }
        IRoleRepository Role { get; }

        Task CreateTransactionAsync();
        Task CommitAsync();
        Task Rollback();
        Task Save();
    }
}
