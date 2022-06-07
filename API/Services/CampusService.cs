using Microsoft.EntityFrameworkCore;
using SECapstoneEvaluation.APIs.DTOs;
using SECapstoneEvaluation.APIs.Services.Constracts;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;
using SECapstoneEvaluation.Domain.Interfaces.UnitOfWork;
using System.Linq;

namespace SECapstoneEvaluation.APIs.Services
{
    public class CampusService : ICampusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampusRepository _campusRepository;

        public CampusService(
            IUnitOfWork unitOfWork,
            ICampusRepository campusRepository)
        {
            _unitOfWork = unitOfWork;
            _campusRepository = campusRepository;
        }
        public async Task<List<LoginCampusViewModel>> GetAllCampusesForLogin()
        {
            return await _campusRepository.List().Select(c => new LoginCampusViewModel ()
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name,
            }).ToListAsync();
        }

        public void ReadExcelFile(IFormFile file)
        {
            
        }
    }
}
