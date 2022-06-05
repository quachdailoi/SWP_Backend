using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SECapstoneEvaluation.Domain.Interfaces.Repositories;
using SECapstoneEvaluation.Domain.Interfaces.UnitOfWork;

namespace SECapstoneEvaluation.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction? _dbTransaction;
        private bool _disposed;
        private readonly ILogger _logger;

        public IUserRepository User { get; private set; }
        public IRoleUserRepository RoleUser { get; private set; }
        public IRoleRepository Role { get; private set; }

        public UnitOfWork(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {
            this._dbContext = dbContext;
            this._logger = loggerFactory.CreateLogger("logs");
        }

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task CreateTransactionAsync()
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Rollback()
        {
            if (_dbTransaction != null)
            {
                await _dbTransaction.RollbackAsync();
                await _dbTransaction.DisposeAsync();
            }
        }

        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                _logger.LogError(dbEx, "{UnitOfWork} Save function error", typeof(UnitOfWork));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();
            _disposed = true;
        }
    }
}
