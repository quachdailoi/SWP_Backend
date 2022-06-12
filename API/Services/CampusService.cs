using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using API.DTOs;
using API.Services.Constracts;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using System.Linq;

namespace API.Services
{
    public class CampusService : ICampusService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICampusRepository campusRepository;

        public CampusService(
            IUnitOfWork unitOfWork,
            ICampusRepository campusRepository)
        {
            this.unitOfWork = unitOfWork;
            this.campusRepository = campusRepository;
        }
        public async Task<List<LoginCampusViewModel>> GetAllCampusesForLogin()
        {
            return await campusRepository.List().Select(c => new LoginCampusViewModel ()
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name,
            }).ToListAsync();
        }        
    }
}
