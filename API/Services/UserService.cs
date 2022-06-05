using Microsoft.EntityFrameworkCore;
using SECapstoneEvaluation.APIs.DTOs;
using SECapstoneEvaluation.APIs.Services.Constracts;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;
using SECapstoneEvaluation.Domain.Interfaces.UnitOfWork;

namespace SECapstoneEvaluation.APIs.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository, 
            IRoleUserRepository roleUserRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleUserRepository = roleUserRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserRoleDto?> GetUserByEmail(string email)
        {      
            var userRoleDto = await (from user in _userRepository.GetUserByEmail(email)
                                     join userRole in _roleUserRepository.List() on user.Id equals userRole.UserId
                                     join role in _roleRepository.List() on userRole.RoleId equals role.Id
                                     select new UserRoleDto
                                     {
                                         Id = user.Id,
                                         Email = email,
                                         CampusId = user.CampusId,
                                         Code = user.Code,
                                         Name = user.Name,
                                         Birthday = user.Birthday,
                                         Phone = user.Phone,
                                         Gender = user.Gender,
                                         RoleId = userRole.RoleId,
                                         RoleName = role.Name
                                     }).FirstOrDefaultAsync();

            return userRoleDto;
        }
    }
}
