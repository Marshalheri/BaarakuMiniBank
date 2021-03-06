using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Transactions;
using BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core
{
    public interface ITransactionService
    {
        Task<BasicResponse> FundCustomerAccountAsync(FundAccountRequestDTO request);
        Task<PayloadResponse<VerifyAccountNumberResponseDTO>> VerifyCustomerAccountAsync(string accountNumber, string bankCode);
        Task<PayloadResponse<IEnumerable<BanksData>>> GetBanksAsync();
    }
}
