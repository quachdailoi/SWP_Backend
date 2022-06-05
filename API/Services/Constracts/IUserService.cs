using SECapstoneEvaluation.APIs.DTOs;

namespace SECapstoneEvaluation.APIs.Services.Constracts
{
    public interface IUserService
    {
        Task<UserRoleDto?> GetUserByEmail(string email);
    }
}
