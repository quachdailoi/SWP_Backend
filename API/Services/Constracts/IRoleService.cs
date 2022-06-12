namespace API.Services.Constracts
{
    public interface IRoleService
    {
        Task<int> GetRoleIdByName(string roleName);
    }
}
