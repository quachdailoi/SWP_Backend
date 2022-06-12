using API.DTOs;
using API.DTOs.Response;
using API.Services.Constracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapstoneTeamsController : ControllerBase
    {
        private readonly ICapstoneTeamService capstoneTeamService;

        public CapstoneTeamsController(ICapstoneTeamService capstoneTeamService)
        {
            this.capstoneTeamService = capstoneTeamService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCapstoneTeam(IFormFile file)
        {
            BaseResponse response = new();
            try
            {
                var capstoneTeams = await capstoneTeamService.ReadExcelFile(file);

                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Create capstone teams by excel file successfully.";
                response.Data = capstoneTeams;
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new(
                    StatusCode: StatusCodes.Status500InternalServerError,
                    Message: ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCapstoneTeams([FromQuery]PaginatedDataViewModel paginatedData)
        {
            BaseResponse response = new(
                    StatusCode: StatusCodes.Status404NotFound,
                    Message: "Not found any capstone team."
                );
            try
            {
                var tupleResult = await capstoneTeamService.GetAllCapstoneTeams(paginatedData);
                var capstoneTeams = tupleResult.Item1;
                if (!capstoneTeams.Any())
                {
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }
                response = new(
                    StatusCode: StatusCodes.Status200OK,
                    Message: "Get capstone teams successfully",
                    Data: new
                    {
                        CapstoneTeams = capstoneTeams,
                        TotalPages = tupleResult.Item2
                    }
                );
                return Ok(response);
            } 
            catch(Exception ex)
            {
                response = new(
                    StatusCode: StatusCodes.Status500InternalServerError,
                    Message: ex.Message
                );
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
