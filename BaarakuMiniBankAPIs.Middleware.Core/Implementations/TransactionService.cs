using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Transactions;
using BaarakuMiniBankAPIs.Middleware.Core.Processors;
using BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack;
using BaarakuMiniBankAPIs.Middleware.Core.Repository;
using BaarakuMiniBankAPIs.Middleware.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly SystemSettings _settings;
        private readonly IMessageProvider _messageProvider;
        private readonly IPayStackProcessor _payStackProcessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(IOptions<SystemSettings> settings, IMessageProvider messageProvider, IUnitOfWork unitOfWork, 
                                  ILogger<TransactionService> logger, IPayStackProcessor payStackProcessor)
        {
            _settings = settings.Value;
            _messageProvider = messageProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _payStackProcessor = payStackProcessor;
        }
        public async Task<BasicResponse> FundCustomerAccountAsync(FundAccountRequestDTO request)
        {
            var response = new BasicResponse(false);
            var account = await _unitOfWork.AccountRepository.GetAsync(x => x.AccountNumber == request.AccountNumber);
            if (account == null)
            {
                return ErrorResponse.Create< BasicResponse> (
                    FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                    ResponseCodes.NO_ACOUNT_FOUND,
                    _messageProvider.GetMessage(ResponseCodes.NO_ACOUNT_FOUND));
            }

            if (account.IsCreditFrozen)
            {
                return ErrorResponse.Create<BasicResponse>(
                    FaultMode.CLIENT_INVALID_ARGUMENT,
                    ResponseCodes.UNABLE_TO_COMPLETE_TRANSACTION,
                    _messageProvider.GetMessage(ResponseCodes.UNABLE_TO_COMPLETE_TRANSACTION));
            }

            account.Balance += request.Amount;
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveAsync();
            response.IsSuccessful = true;
            return response;
        }

        public async Task<PayloadResponse<IEnumerable<BanksData>>> GetBanksAsync()
        {
            var response = new PayloadResponse<IEnumerable<BanksData>>(false);
            var serviceResponse = await _payStackProcessor.GetBanksAsync();
            if (!serviceResponse.IsSuccessful)
            {
                return ErrorResponse.Create<PayloadResponse<IEnumerable<BanksData>>>(
                    FaultMode.CLIENT_INVALID_ARGUMENT,
                    serviceResponse.Error.ErrorCode,
                    serviceResponse.Error.Description);
            }

            response.SetPayload(serviceResponse.GetPayload());
            response.IsSuccessful = true;
            return response;
        }

        public async Task<PayloadResponse<VerifyAccountNumberResponseDTO>> VerifyCustomerAccountAsync(string accountNumber, string bankCode)
        {
            var response = new PayloadResponse<VerifyAccountNumberResponseDTO>(false);

            if (bankCode == _settings.BankCode)
            {
                var account = await _unitOfWork.AccountRepository.GetAsync(x => x.AccountNumber == accountNumber, includes: x => x.Customer);
                if (account == null)
                {
                    return ErrorResponse.Create<PayloadResponse<VerifyAccountNumberResponseDTO>>(
                        FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                        ResponseCodes.NO_ACOUNT_FOUND,
                        _messageProvider.GetMessage(ResponseCodes.NO_ACOUNT_FOUND));
                }

                response.SetPayload(new VerifyAccountNumberResponseDTO { AccountName = $"{account.Customer.FirstName} {account.Customer.LastName}" });
            }
            else
            {
                var serviceResponse = await _payStackProcessor.VerifyAccountNumberAsync(accountNumber, bankCode);
                if (!serviceResponse.IsSuccessful)
                {
                    return ErrorResponse.Create<PayloadResponse<VerifyAccountNumberResponseDTO>>(
                        FaultMode.CLIENT_INVALID_ARGUMENT,
                        serviceResponse.Error.ErrorCode,
                        serviceResponse.Error.Description);
                }
                response.SetPayload(serviceResponse.GetPayload());
            }

            response.IsSuccessful = true;
            return response;
        }
    }
}
