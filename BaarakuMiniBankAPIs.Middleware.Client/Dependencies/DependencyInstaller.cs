using BaarakuMiniBankAPIs.Middleware.Core;
using BaarakuMiniBankAPIs.Middleware.Core.Fakes;
using BaarakuMiniBankAPIs.Middleware.Core.Implementations;
using BaarakuMiniBankAPIs.Middleware.Core.Processors;
using BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack;
using BaarakuMiniBankAPIs.Middleware.Core.Repository;
using BaarakuMiniBankAPIs.Middleware.Core.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaarakuMiniBankAPIs.Middleware.Client.Dependencies
{
    public static class DependencyInstaller
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Services 
            services.AddScoped<IMessageProvider, MessageProvider>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITransactionService, TransactionService>();

            // DAOs
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            if (configuration.GetValue<bool>("SystemSettings:UseFake"))
            {
                //FakeProcessors
                services.AddScoped<IMessagePackProvider, FakeMessagePackProvider>();
                services.AddScoped<IPayStackProcessor, FakePayStackProcessor>();
            }
            else
            {
                //Processors
                services.AddScoped<IMessagePackProvider, FakeMessagePackProvider>();
                services.AddScoped<IPayStackProcessor, PayStackProcessor>();
            }

            //Filters
            services.Configure<SystemSettings>(opt => configuration.GetSection("SystemSettings").Bind(opt));
            services.Configure<MessagePackSettings>(opt => configuration.GetSection("MessagePackSettings").Bind(opt));
            services.Configure<PaystackSettings>(opt => configuration.GetSection("PaystackSettings").Bind(opt));


            services.AddDbContext<ApplicationContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        }
    }
}
