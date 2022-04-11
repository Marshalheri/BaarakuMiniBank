using BaarakuMiniBankAPIs.Middleware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BaarakuMiniBankAPIs.Middleware.Core.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {

        public AccountRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
