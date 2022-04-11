using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Customers;
using BaarakuMiniBankAPIs.Middleware.Core.Models;
using BaarakuMiniBankAPIs.Middleware.Core.Repository;
using BaarakuMiniBankAPIs.Middleware.Core.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly SystemSettings _settings;
        private readonly IMessageProvider _messageProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IOptions<SystemSettings> settings, IMessageProvider messageProvider, IUnitOfWork unitOfWork, ILogger<CustomerService> logger)
        {
            _settings = settings.Value;
            _messageProvider = messageProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<PayloadResponse<CreateCustomerResponseDTO>> CreateCustomer(CreateCustomerRequestDTO request)
        {
            PayloadResponse<CreateCustomerResponseDTO> response = new(false);
            Customer customer = new()
            {
                DateCreated = DateTime.Now,
                EmailAddress = request.EmailAddress,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                IsActive = true
            };
            await _unitOfWork.BeginTransactionAsync();
            var customerCreated = false;
            while (!customerCreated)
            {
                try
                {
                    customer.CustomerId = $"{_settings.CustomerIdPrefix}{Util.GenerateNumbers(7, 0, 9)}";
                    await _unitOfWork.CustomerRepository.AddAsync(customer);
                    await _unitOfWork.SaveAsync();
                    Image image = new()
                    {
                        CustomerId = customer.Id,
                        Extension = request.Image.Extension,
                        RawData = request.Image.RawData,
                        DateCreated = customer.DateCreated,
                        IsActive = true
                    };
                    await _unitOfWork.ImageRepository.AddAsync(image);
                    await _unitOfWork.SaveAsync();
                    customerCreated = true;
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex.ToString());
                    if (ex.Number != 2601 || ex.Number != 2627)
                    {
                        break;
                    }
                }
            }
            var accountCreated = false;
            Account account = new()
            {
                CustomerId = customer.Id,
                Balance = 0,
                DateCreated = DateTime.Now,
                IsDebitFrozen = false,
                IsActive = true,
                IsCreditFrozen = false
            };
            while (!accountCreated)
            {
                try
                {
                    account.AccountNumber = $"{_settings.AccountNumberPrefix}{Util.GenerateNumbers(7, 0, 9)}";
                    await _unitOfWork.AccountRepository.AddAsync(account);
                    await _unitOfWork.SaveAsync();
                    accountCreated = true;
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex.ToString());
                    if (ex.Number != 2601 || ex.Number != 2627)
                    {
                        break;
                    }
                }
            }
            await _unitOfWork.CommitAsync();
            response.SetPayload(new CreateCustomerResponseDTO { AccountNumber = account.AccountNumber });
            response.IsSuccessful = true;
            return response;
        }
    }
}
