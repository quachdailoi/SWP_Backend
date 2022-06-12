using API.DTOs;

namespace API.DTOs.Response
{
    public class UserLoginSuccessResponse
    {
        public string AccessToken { get; set; }
        public UserRolesDto User { get; set; }
    }
}
