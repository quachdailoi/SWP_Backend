using Domain.Entities;

namespace API.Services.Constracts
{
    public interface IRoleUserService
    {
        Task<IEnumerable<RoleUser>> GetAll();

        Task<RoleUser> CreateRoleUser(RoleUser roleUser);
    }
}
