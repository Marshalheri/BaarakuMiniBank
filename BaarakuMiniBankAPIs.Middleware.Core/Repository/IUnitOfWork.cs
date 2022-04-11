using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Repository
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IAccountRepository AccountRepository { get; }
        IImageRepository ImageRepository { get; }
        Task BeginTransactionAsync();
        Task SaveAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
