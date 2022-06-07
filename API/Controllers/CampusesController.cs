using API.DTOs.Response;
using API.Helpers.CsvMapper;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using SECapstoneEvaluation.APIs.Services.Constracts;
using SECapstoneEvaluation.Domain.Entities;
using System.Globalization;
using System.Net;
using System.Text;

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

            BaseResponse response = new BaseResponse
            {
                Message = "Get campuses successfully.",
                Data = campuses
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampuses([FromForm]IFormFile file)
        {
            List<Campus> campuses = new();
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.Default))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<CampusMap>();
                campuses = csvReader.GetRecords<Campus>().ToList();
            }

            return Ok(campuses);
        }
    }
}
