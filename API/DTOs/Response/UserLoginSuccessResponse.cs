using SECapstoneEvaluation.APIs.DTOs;

namespace API.DTOs.Response
{
    public class UserLoginSuccessResponse
    {
        public string AccessToken { get; set; }
        public UserRoleDto User { get; set; }
    }
}
