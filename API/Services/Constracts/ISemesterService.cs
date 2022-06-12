using Domain.Entities;

namespace API.Services.Constracts
{
    public interface ISemesterService
    {
        Task<bool> AnySemesterWithCode(string code);
        Task<IEnumerable<Semester>> GetAllSemesters();
        Task<Semester?> GetExistedSemesterByCode(string code);
    }
}
