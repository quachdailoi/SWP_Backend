using API.Services.Constracts;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository semesterRepository;

        public SemesterService(ISemesterRepository semesterRepository)
        {
            this.semesterRepository = semesterRepository;
        }
        public async Task<bool> AnySemesterWithCode(string code)
        {
            return await GetExistedSemesterByCode(code) == null;
        }

        public async Task<IEnumerable<Semester>> GetAllSemesters()
        {
            return await semesterRepository.List().ToListAsync();
        }

        public Task<Semester?> GetExistedSemesterByCode(string code)
        {
            return semesterRepository.GetSemesterByCode(code);
        }
    }
}
