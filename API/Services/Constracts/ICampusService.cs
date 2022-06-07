using SECapstoneEvaluation.APIs.DTOs;

namespace SECapstoneEvaluation.APIs.Services.Constracts
{
    public interface ICampusService
    {
        Task<List<LoginCampusViewModel>> GetAllCampusesForLogin();

        void ReadExcelFile(IFormFile file);
    }
}
