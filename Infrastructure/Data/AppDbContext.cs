using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data.EntityConfigurations;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CampusEntityConfiguration()
                .Configure(modelBuilder.Entity<Campus>());

            new RoleEntityConfiguration()
                .Configure(modelBuilder.Entity<Role>());

            new UserEntityConfiguration()
                .Configure(modelBuilder.Entity<User>());

            new RoleUserEntityConfiguration()
                .Configure(modelBuilder.Entity<RoleUser>());

            new TopicEntityConfiguration()
                .Configure(modelBuilder.Entity<Topic>());

            new SemesterEntityConfiguration()
                .Configure(modelBuilder.Entity<Semester>());

            new CapstoneTeamEntityConfiguration()
                .Configure(modelBuilder.Entity<CapstoneTeam>());

            modelBuilder.Entity<Campus>().HasData(new Campus 
            {
                Id = 1,
                Name = "FPT University Ha Noi",
                Code = "FPTU HN",
            });

            modelBuilder.Entity<Campus>().HasData(new Campus
            {
                Id = 2,
                Name = "FPT University Ho Chi Minh",
                Code = "FPTU HCM",
            });

            modelBuilder.Entity<Campus>().HasData(new Campus
            {
                Id = 3,
                Name = "FPT University Da Nang",
                Code = "FPTU DN",
            });

            modelBuilder.Entity<Campus>().HasData(new Campus
            {
                Id = 4,
                Name = "FPT University Can Tho",
                Code = "FPTU CT",
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Student"
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 2,
                Name = "Lecture"
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 3,
                Name = "Leader"
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 4,
                Name = "Member"
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 5,
                Name = "Mentor"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Code = "SE140970",
                Name = "Quach Dai Loi",
                Birthday = DateTime.UtcNow,
                CampusId = 2,
                Email = "loiqdse140970@fpt.edu.vn",
                Phone = "0837226239",
                Gender = false,
                Status = true,
                
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Code = "SE140977",
                Name = "Nguyen Dang Khoa",
                Birthday = DateTime.UtcNow,
                CampusId = 2,
                Email = "khoandse140977@fpt.edu.vn",
                Phone = "0123123123",
                Gender = false,
                Status = true,

            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 3,
                Code = "SE140971",
                Name = "Than Thanh Duy",
                Birthday = DateTime.UtcNow,
                CampusId = 2,
                Email = "duyttse140971@fpt.edu.vn",
                Phone = "0123123123",
                Gender = false,
                Status = true,

            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 4,
                Code = "SE140920",
                Name = "Do Trong Dat",
                Birthday = DateTime.UtcNow,
                CampusId = 2,
                Email = "datdtse140920@fpt.edu.vn",
                Phone = "0123123123",
                Gender = false,
                Status = true,

            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 5,
                Code = "HuongNTC2",
                Name = "Nguyen Thi Cam Huong",
                Birthday = DateTime.UtcNow,
                CampusId = 2,
                Email = "huongntc2@fpt.edu.vn",
                Phone = "0123123123",
                Gender = true,
                Status = true,
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 6,
                Code = "PhuongLHK",
                Name = "Lam Huu Khanh Phuong",
                Birthday = DateTime.UtcNow,
                CampusId = 2,
                Email = "phuonglhk@fpt.edu.vn",
                Phone = "0123123123",
                Gender = false,
                Status = true,
            });

            for (int i = 0; i < 200; i++)
            {
                var code = 140500;
                
                modelBuilder.Entity<User>().HasData(new User
                {
                    Id = 7+i,
                    Code = $"SE{code+i}",
                    Name = $"Nguyen Van A{i}",
                    Birthday = DateTime.UtcNow,
                    CampusId = 2,
                    Email = $"a{i}nv@fpt.edu.vn",
                    Phone = $"012312312{i}",
                    Gender = false,
                    Status = true,
                });
            }

            #region RoleUsers
            modelBuilder.Entity<RoleUser>().HasData(new RoleUser
            {
                Id = 1,
                RoleId = 1,
                UserId = 1
            });

            modelBuilder.Entity<RoleUser>().HasData(new RoleUser
            {
                Id = 2,
                RoleId = 1,
                UserId = 2
            });

            modelBuilder.Entity<RoleUser>().HasData(new RoleUser
            {
                Id = 3,
                RoleId = 1,
                UserId = 3
            });

            modelBuilder.Entity<RoleUser>().HasData(new RoleUser
            {
                Id = 4,
                RoleId = 1,
                UserId = 4
            });

            modelBuilder.Entity<RoleUser>().HasData(new RoleUser
            {
                Id = 5,
                RoleId = 2,
                UserId = 5
            });

            modelBuilder.Entity<RoleUser>().HasData(new RoleUser
            {
                Id = 6,
                RoleId = 2,
                UserId = 6
            });

            for(int i = 0; i < 200; i++)
            {
                modelBuilder.Entity<RoleUser>().HasData(new RoleUser
                {
                    Id = 7+i,
                    RoleId = 1,
                    UserId = 7+i
                });
            }
            #endregion

            #region Topics
            for(int i = 1; i <= 200; i++)
            {
                modelBuilder.Entity<Topic>().HasData(new Topic
                {
                    Id = i,
                    Name = $"Capstone Topic {i}",
                    Code = $"SEC{i}.2022FALL",
                    Description = "aaaaaaa"
                });
            }
            #endregion

            #region Semesters
            modelBuilder.Entity<Semester>().HasData(new Semester
            {
                Id = 1,
                Name = "Fall semester of 2022",
                Code = "2022FALL",
                StartAt = DateTime.UtcNow,
                EndAt = DateTime.UtcNow.AddDays(30 * 4)
            });
            #endregion
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<CapstoneTeam> CapstoneTeams { get; set; }
    }
}
