using API.DTOs;
using Domain.Entities;

namespace API.Services.Constracts
{
    public interface ICampusService
    {
        Task<List<LoginCampusViewModel>> GetAllCampusesForLogin();
    }
}
