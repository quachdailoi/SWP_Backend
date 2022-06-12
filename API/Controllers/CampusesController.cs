using API.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using API.Services.Constracts;

namespace SECapstoneEvaluation.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusesController : ControllerBase
    {
        private readonly ICampusService campusService;

        public CampusesController(ICampusService campusService)
        {
            this.campusService = campusService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var campuses = await campusService.GetAllCampusesForLogin();

            BaseResponse response = new();

            if (campuses == null || !campuses.Any())
            {
                response.SetStatusCode(StatusCodes.Status404NotFound)
                    .SetMessage("Not found any campuses.");
                return NotFound(response);
            }

            response = new(
                Message: "Get campuses successfully.",
                StatusCode: StatusCodes.Status200OK,
                Data: campuses
            );

            return Ok(response);
        }
    }
}
