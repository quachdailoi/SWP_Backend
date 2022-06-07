using API.DTOs.Response;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SECapstoneEvaluation.APIs.Services.Constracts;
using SECapstoneEvaluation.Domain.Entities;
using System.Data;
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

            BaseResponse response = new();

            if (campuses == null || !campuses.Any())
            {
                response.StatusCode(StatusCodes.Status404NotFound)
                    .Message("Not found any campuses.");
                return NotFound();
            }

            response.StatusCode(StatusCodes.Status200OK)
                .Message("Get campuses successfully.")
                .Data(campuses);

            return Ok(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCampuses([FromForm] IFormFile file)
        //{
        //    Stream stream = file.OpenReadStream();

        //    if (file == null || stream == null)
        //    {
        //        return BadRequest("Seleted file is empty.");
        //    }

        //    bool isValidFileExtension = file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx");
        //    if (!isValidFileExtension)
        //    {
        //        return BadRequest("The file format is not supported.");
        //    }
        //    FileStream fileStream = stream as FileStream;

        //    IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(fileStream);

        //    DataSet dsExcelRecords = reader.AsDataSet();

        //    reader.Close();

        //    if (dsExcelRecords == null || dsExcelRecords.Tables.Count <= 0)
        //    {
                
        //    }

        //    return Ok("ok");
        //}
    }
}
