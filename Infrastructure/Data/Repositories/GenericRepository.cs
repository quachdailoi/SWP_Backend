using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SECapstoneEvaluation.Domain.Interfaces.Entities;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace SECapstoneEvaluation.Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _dbContext;

        public DbSet<T> DbSet { get; private set; }

        private readonly ILogger _logger;

        public GenericRepository(AppDbContext dbContext, ILogger<GenericRepository<T>> logger)
        {
            this._dbContext = dbContext;
            this.DbSet = dbContext.Set<T>();
            this._logger = logger;
        }

        public async Task Add(T entity)
        {
            try
            {
                if (typeof(IBaseEntity).IsAssignableFrom(typeof(T)))
                {
                    ((IBaseEntity) entity).CreatedAt = DateTime.UtcNow;
                }
                await DbSet.AddAsync(entity);

                await this._dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add function error", typeof(T));
            }
        }

        public IQueryable<T> List(Expression<Func<T, bool>>? expression = null)
        {
            if (expression == null)
            {
                return DbSet.AsQueryable();
            }
            return DbSet.Where(expression);
        }

        public virtual async Task<bool> Remove(T entity)
        {
            try
            {
                var exist = await DbSet.FindAsync((IBaseEntity) entity);

                if (exist == null) return false;

                if (typeof(IBaseEntity).IsAssignableFrom(typeof(T)))
                {
                    ((IBaseEntity) entity).IsDeleted = true;
                    DbSet.Update(entity);
                }
                else
                {
                    DbSet.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Remove function error", typeof(T));
                return false;
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task Update(T entity)
        {
            try
            {
                if (typeof(IBaseEntity).IsAssignableFrom(typeof(T)))
                {
                    ((IBaseEntity)entity).UpdatedAt = DateTime.UtcNow;
                }
                DbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(T));
            }
        }
    }
}
