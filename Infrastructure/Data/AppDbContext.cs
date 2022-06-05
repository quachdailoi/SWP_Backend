using Microsoft.EntityFrameworkCore;
using SECapstoneEvaluation.Domain.Entities;
using SECapstoneEvaluation.Infrastructure.Data.EntityConfigurations;

namespace SECapstoneEvaluation.Infrastructure.Data
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

            modelBuilder.Entity<RoleUser>().HasData(new RoleUser
            {
                Id = 1,
                RoleId = 1,
                UserId = 1
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
    }
}
