using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;

namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext dbContext;
        private IDbContextTransaction? dbTransaction;
        private bool disposed;
        private readonly ILogger logger;

        public IUserRepository User { get; private set; }
        public IRoleUserRepository RoleUser { get; private set; }
        public IRoleRepository Role { get; private set; }

        public ITopicRepository Topic { get; set; }
        public ISemesterRepository Semester { get; set; }
        public ICapstoneTeamRepository CapstoneTeam { get; set; }

        public UnitOfWork(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this.dbContext = dbContext;
            this.logger = loggerFactory.CreateLogger("logs");
        }

        public async Task CommitAsync()
        {
            if (dbTransaction != null)
            {
                await dbTransaction.CommitAsync();
            }
        }

        public async Task CreateTransactionAsync()
        {
            try
            {
                dbTransaction = await dbContext.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Rollback()
        {
            if (dbTransaction != null && !disposed)
            {
                await dbTransaction.RollbackAsync();
                await dbTransaction.DisposeAsync();
                disposed = true;
            }
        }

        public async Task Save()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                logger.LogError(dbEx, "{UnitOfWork} Save function error", typeof(UnitOfWork));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    dbContext.Dispose();
            disposed = true;
        }
    }
}
