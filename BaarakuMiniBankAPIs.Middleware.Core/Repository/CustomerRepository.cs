using BaarakuMiniBankAPIs.Middleware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BaarakuMiniBankAPIs.Middleware.Core.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
