using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Services.Constracts;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using Domain.Entities;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IRoleUserService roleUserService;

        public UserService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IRoleUserService roleUserService
            )
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.roleUserService = roleUserService;
        }

        public Task<UserRolesDto?> GetExistDataByCode(string code)
        {
            return GetUserRolesDtoByCode(code);
        }

        public async Task<UserRolesDto?> GetUserRolesDtoByCode(string code)
        {
            var users = userRepository.GetUserByCode(code);
            var userRole = await GetUserRole(users).FirstOrDefaultAsync();
            return userRole;
        }

        public async Task<UserRolesDto?> GetUserRolesDtoByEmail(string email)
        {
            var users = userRepository.GetUserByEmail(email);
            var userRole = await GetUserRole(users).FirstOrDefaultAsync();
            return userRole;
        }

        public IEnumerable<UserRolesDto> GetUserRolesDtosByUserIds(List<int> ids)
        {
            return GetUserRole(userRepository.List(user => ids.Contains(user.Id))).ToList();
        }

        private IQueryable<UserRolesDto> GetUserRole(IQueryable<User> users)
        {
            return users.Select(user => new UserRolesDto
            {
                Id = user.Id,
                Email = user.Email,
                CampusId = user.CampusId,
                Code = user.Code,
                Name = user.Name,
                Birthday = user.Birthday,
                Phone = user.Phone,
                Gender = user.Gender,
                Roles = user.RoleUsers.Select(ru => new RoleDto { RoleId = ru.RoleId, RoleName = ru.Role.Name }).ToList(),
            });
        }


    }
}
