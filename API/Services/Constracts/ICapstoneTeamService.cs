using API.DTOs;
using Domain.Entities;

namespace API.Services.Constracts
{
    public interface ICapstoneTeamService
    {
        Task<List<CreateCapstoneTeamDto>> ReadExcelFile(IFormFile file);

        Task<Tuple<IEnumerable<CapstoneTeamViewModel>, int>> GetAllCapstoneTeams(PaginatedDataViewModel paginatedData);
    }
}
