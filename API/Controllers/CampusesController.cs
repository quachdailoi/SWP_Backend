using Microsoft.AspNetCore.Mvc;
using SECapstoneEvaluation.APIs.Services.Constracts;
using System.Net;

namespace SECapstoneEvaluation.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusesController : ControllerBase
    {
        private readonly ICampusService _campusService;

        public CampusesController(ICampusService campusService)
        {
            _campusService = campusService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var campuses = await _campusService.GetAllCampusesForLogin();

            if (campuses == null || !campuses.Any())
            {
                return NotFound();
            }

            return Ok(campuses);
        }
    }
}
