using API.DTOs;
using API.Services.Constracts;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text;

namespace API.Services
{
    public class CapstoneTeamService : ICapstoneTeamService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICapstoneTeamRepository capstoneTeamRepository;
        private readonly IUserService userService;
        private readonly ISemesterService semesterService;
        private readonly ITopicService topicService;
        private readonly IRoleService roleService;
        private readonly IRoleUserService roleUserService;

        public CapstoneTeamService(IUnitOfWork unitOfWork, 
            ICapstoneTeamRepository capstoneTeamRepository, 
            IUserService userService, 
            ISemesterService semesterService, 
            ITopicService topicService, 
            IRoleService roleService,
            IRoleUserService roleUserService
            )
        {
            this.unitOfWork = unitOfWork;
            this.capstoneTeamRepository = capstoneTeamRepository;
            this.userService = userService;
            this.semesterService = semesterService;
            this.topicService = topicService;
            this.roleService = roleService;
            this.roleUserService = roleUserService;
        }

        public async Task<List<CreateCapstoneTeamDto>> ReadExcelFile(IFormFile file)
        {
            var list = new List<CreateCapstoneTeamDto>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    
                    try
                    {
                        List<string> studentCodes = new();
                        List<string> topicCodes = new();
                        // open transaction
                        await unitOfWork.CreateTransactionAsync();
                        
                        for (int row = 2; row <= rowcount; row++)
                        {
                            try
                            {
                                var tupleResult = await ReflectionSetValueByRow(worksheet, row, studentCodes, topicCodes);
                                var createDto = tupleResult.Item1;
                                studentCodes = tupleResult.Item2;
                                topicCodes = tupleResult.Item3;
                                list.Add(createDto);
                            }
                            catch (Exception ex)
                            {
                                await unitOfWork.Rollback();
                                throw new Exception(ex.Message);
                            }
                        }
                        
                        // check duplicated students code for the whole excel file
                        var duplicatedCodeStrs = CheckDuplicateCodes(studentCodes);
                        if (duplicatedCodeStrs != null)
                        {
                            throw new Exception($"Student code: {duplicatedCodeStrs} appear(s) in more than 1 team.");
                        }

                        // check duplicated topics code for the whole excel file
                        var duplicatedTopicCodeStrs = CheckDuplicateCodes(topicCodes);
                        if (duplicatedTopicCodeStrs != null)
                        {
                            throw new Exception($"Topic code: {duplicatedCodeStrs} appear(s) in more than 1 team.");
                        }
                    } 
                    catch (Exception ex)
                    {
                        await unitOfWork.Rollback();
                        throw new Exception(ex.Message);
                    }
                    // commit if everything is OK
                    await unitOfWork.CommitAsync();
                }
            }

            return list;
        }

        private async Task<Tuple<CreateCapstoneTeamDto, List<string>, List<string>>> ReflectionSetValueByRow(ExcelWorksheet worksheet, int row, List<string> studentCodes, List<string> topicCodes)
        {
            ExcelCreateCapstoneTeamViewModel viewModel = new();
            CreateCapstoneTeamDto dto = new();
            var columnCount = worksheet.Dimension.Columns;

            Type viewModelType = typeof(ExcelCreateCapstoneTeamViewModel);
            var properties = viewModelType.GetProperties();
            var propertiesNumber = properties.Length;

            if (propertiesNumber - 2 != columnCount)
            {
                throw new Exception($"Excel file does not have available format (number of columns invalid - invalid row: {row}).");
            }
            List<string> teamStudentCodes = new();
            for (int column = 1; column <= columnCount; column++)
            {
                var cellValue = worksheet.Cells[row, column].Value;

                if (cellValue != null)
                {
                    var code = cellValue.ToString().Trim();
                    
                    properties[column - 1].SetValue(viewModel, code);

                    //check code is belong to a user
                    UserRolesDto? userRolesDto = await userService.GetUserRolesDtoByCode(code);
                    if (userRolesDto == null && column <= 7)
                    {
                        throw new Exception($"Not found user with code: {code} at row: {row}");
                    }

                    if (column >= 1 && column <= 5) // check user have student role
                    {
                        CheckUserCodeMatchRole(userRolesDto, code, "Student", row);

                        studentCodes.Add(code);
                        teamStudentCodes.Add(code);
                        if (column == 1)
                        {
                            dto.LeaderId = userRolesDto.Id; // role leader
                        } 
                        else
                        {
                            dto.MemberIds.Add(userRolesDto.Id); // role member
                        }
                    }
                    else if (column == 6 || column == 7) // check user have lecture role
                    {
                        CheckUserCodeMatchRole(userRolesDto, code, "Lecture", row);

                        if (viewModel.Mentor1Code == viewModel.Mentor2Code)
                        {
                            throw new Exception($"Mentor with code: {code} is duplicated, at row: {row}");
                        }

                        dto.MentorIds.Add(userRolesDto.Id);
                    }
                    else if (column == 8) // check exist topic code
                    {
                        var existedTopic = await topicService.GetExistedTopicByCode(code);
                        if (existedTopic == null)
                        {
                            throw new Exception($"Not found topic with code: {code}, at row: {row}");
                        }
                        topicCodes.Add(code);
                        dto.TopicId = existedTopic.Id;
                    }
                    else if (column == 9) // check exist semester code
                    {
                        var existedSemester = await semesterService.GetExistedSemesterByCode(code);
                        if (existedSemester == null)
                        {
                            throw new Exception($"Not found semester with code: {code}, at row: {row}");
                        }
                        dto.SemesterId = existedSemester.Id;
                    }
                }
            }

            var duplicatedStudentCodeInTeamStrs = CheckDuplicateCodes(teamStudentCodes);
            if (duplicatedStudentCodeInTeamStrs != null)
            {
                throw new Exception($"Duplicate code: {duplicatedStudentCodeInTeamStrs} in row: {row}");
            }

            // temp creating 1 team, roll back if failure
            var teamId = await CreateCapstoneTeam(dto);
            dto.TeamId = teamId;
            await AddUsersToTeam(dto, row);

            return Tuple.Create(dto, studentCodes, topicCodes);
        }
        
        private string? CheckDuplicateCodes(List<string> codes)
        {
            string? duplicatedCodeStrs = null;
            var duplicatedCodes = codes.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();

            if (duplicatedCodes.Any())
            {
                string duplicateCodeStrs = "";
                foreach (var code in duplicatedCodes)
                {
                    duplicateCodeStrs += code + ";";
                }

                return duplicateCodeStrs;
            }
            return duplicatedCodeStrs;
        }

        private void CheckUserCodeMatchRole(UserRolesDto dto, string code, string roleName, int row)
        {
            var isStudentRole = dto.Roles.Where(role => role.RoleName.ToLower() == roleName.ToLower()).Any();
            if (!isStudentRole)
            {
                throw new Exception($"User with code: {code} is not a {roleName}, at row: {row}");
            }
        }

        public async Task<Tuple<IEnumerable<CapstoneTeamViewModel>, int>> GetAllCapstoneTeams(PaginatedDataViewModel paginatedData)
        {
            var teams = capstoneTeamRepository.GetAll();
            if (!teams.Any())
            {
                return Tuple.Create(Enumerable.Empty<CapstoneTeamViewModel>(), 0);
            }
            paginatedData.GenerateSkipAndTotalPage(teams.Count());

            var totalPages = paginatedData.GetTotalPages();
            
            var data = await teams.Select(team => new CapstoneTeamViewModel()
            {
                TeamId = team.Id,
                Semester = new SemesterCapstoneTeamViewModel()
                {
                    Id = team.SemesterId,
                    Code = team.Semester.Code,
                    Name = team.Semester.Name,
                },
                Topic = new TopicCapstoneTeamViewModel()
                {
                    Id = team.TopicId,
                    Code = team.Topic.Code ?? "",
                    Description = team.Topic.Description ?? "",
                    Name = team.Topic.Name ?? "",
                },
                Users = team.RoleUsers.Select(userRole => new UserCapstoneTeamViewModel()
                {
                    Id = userRole.UserId,
                    Code = userRole.User.Code,
                    Name = userRole.User.Name,
                    RoleName = userRole.Role.Name
                }).ToList()
            }).OrderByDescending(team => team.Semester.Code)
            .Skip(paginatedData.GetSkip()).Take(paginatedData.GetPageSizes())
            .ToListAsync();

            
            return Tuple.Create(data as IEnumerable<CapstoneTeamViewModel>, totalPages);
        }

        private async Task<int> CreateCapstoneTeam(CreateCapstoneTeamDto dto)
        {
            string teamCode = dto.SemesterId + "_" + dto.TopicId;

            var team = new CapstoneTeam
            {
                Code = teamCode,
                TopicId = dto.TopicId,
                SemesterId = dto.SemesterId,
                Status = 1,
            };

            try
            {
                await capstoneTeamRepository.Add(team);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return team.Id;
        }

        private async Task<int> AddRoleForUser(int userId, int teamId, string roleName)
        {
            var roleId = await roleService.GetRoleIdByName(roleName);

            var userRole = new RoleUser
            {
                RoleId = roleId,
                UserId = userId,
                CapstoneTeamId = teamId,
            };

            return (await roleUserService.CreateRoleUser(userRole)).Id;
        }

        private async Task AddUsersToTeam(CreateCapstoneTeamDto dto, int row)
        {
            // leader
            if (dto.LeaderId == 0)
            {
                throw new Exception($"Capstone teams must has leader code. Invalid at row: {row}");
            }
            await AddRoleForUser(dto.LeaderId, dto.TeamId, "Leader");

            // members 
            foreach (var id in dto.MemberIds)
            {
                await AddRoleForUser(id, dto.TeamId, "Member");
            }

            // mentor
            if (!dto.MentorIds.Any())
            {
                throw new Exception($"Capstone teams must has at least 1 mentor. Invalid at row: {row}");
            }
            foreach (var id in dto.MentorIds)
            {
                await AddRoleForUser(id, dto.TeamId, "Mentor");
            }
        }

        private string GenerateRandomStr(int length)
        {
            if (length < 1)
            {
                throw new Exception("Generate Random Error");
            }
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            return str_build.ToString();
        }
    }
}
