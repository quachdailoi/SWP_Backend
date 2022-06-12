using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class SemesterRepository : GenericRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(AppDbContext dbContext, ILogger<GenericRepository<Semester>> logger) : base(dbContext, logger)
        {
        }

        public Task<Semester?> GetSemesterByCode(string code)
        {
            return this.List(semester => semester.Code == code).FirstOrDefaultAsync();
        }
    }
}
