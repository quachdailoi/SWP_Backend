using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CapstoneTeamRepository : GenericRepository<CapstoneTeam>, ICapstoneTeamRepository
    {
        public CapstoneTeamRepository(AppDbContext dbContext, ILogger<GenericRepository<CapstoneTeam>> logger) : base(dbContext, logger)
        {
        }

        public IQueryable<CapstoneTeam> GetAll()
        {
            return List();
        }
    }
}
