using API.DTOs;
using Domain.Entities;

namespace API.Services.Constracts
{
    public interface IUserService 
    {
        Task<UserRolesDto?> GetUserRolesDtoByEmail(string email);

        Task<UserRolesDto?> GetUserRolesDtoByCode(string code);

        IEnumerable<UserRolesDto> GetUserRolesDtosByUserIds(List<int> ids);
    }
}
