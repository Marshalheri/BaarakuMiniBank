using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposedValue = false;
        private IDbContextTransaction _transaction;

        public ApplicationContext Context { get; }
        public ICustomerRepository CustomerRepository => new CustomerRepository(Context);
        public IAccountRepository AccountRepository => new AccountRepository(Context);
        public IImageRepository ImageRepository => new ImageRepository(Context);

        public UnitOfWork(ApplicationContext context)
        {
            Context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false);
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync().ConfigureAwait(false);
            Dispose();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync().ConfigureAwait(false);
            Dispose();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing && Context != null)
                {
                    Context.Dispose();
                }

                _disposedValue = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
