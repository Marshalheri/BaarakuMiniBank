using BaarakuMiniBankAPIs.Middleware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BaarakuMiniBankAPIs.Middleware.Core.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Customer>().HasIndex(x => x.CustomerId).IsUnique();
            model.Entity<Account>().HasIndex(x => x.AccountNumber).IsUnique();
        }
    }
}
