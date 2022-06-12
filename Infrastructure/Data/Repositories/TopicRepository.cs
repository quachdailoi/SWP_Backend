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
    public class TopicRepository : GenericRepository<Topic>, ITopicRepository
    {
        public TopicRepository(AppDbContext dbContext, ILogger<GenericRepository<Topic>> logger) : base(dbContext, logger)
        {
        }

        public async Task<Topic?> GetTopicByCode(string code)
        {
            return await this.List(topic => topic.Code == code).FirstOrDefaultAsync();
        }
    }
}
