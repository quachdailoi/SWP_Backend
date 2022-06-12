using API.DTOs.Request;
using API.DTOs.Response;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.JwtFeatures;
using API.Services.Constracts;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly JwtHandler jwtHandler;

        public UsersController(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
            jwtHandler = new JwtHandler(configuration);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;

            FirebaseToken verifiedIdToken = await firebaseAuth.VerifyIdTokenAsync(request.IdToken);

            BaseResponse response = new();

            if (verifiedIdToken == null)
            {
                response = new(
                    StatusCode: StatusCodes.Status400BadRequest,
                    Message: "Login failed."
                );
                return BadRequest(response);
            }

            IReadOnlyDictionary<string, dynamic> claims = verifiedIdToken.Claims;

            string email = claims["email"];

            var user = await userService.GetUserRolesDtoByEmail(email);

            if (user == null)
            {
                response.SetStatusCode(StatusCodes.Status400BadRequest)
                    .SetMessage("Login failed - Not found email account in our system.");
                return BadRequest(response);
            }

            if (user.CampusId != request.CampusId)
            {
                response.SetStatusCode(StatusCodes.Status400BadRequest)
                    .SetMessage("Login failed - User account is not access to this campus.");
                return BadRequest(response);
            }

            string token = jwtHandler.GenerateToken(user);

            response.SetStatusCode(StatusCodes.Status200OK)
                .SetMessage("Login successfully.")
                .SetData(new UserLoginSuccessResponse
                {
                    AccessToken = token,
                    User = user
                });

            return new JsonResult(response);
        }

        [Authorize]
        [HttpGet("test-auth")]
        public void Test()
        {
            return;
        }
    }
}
