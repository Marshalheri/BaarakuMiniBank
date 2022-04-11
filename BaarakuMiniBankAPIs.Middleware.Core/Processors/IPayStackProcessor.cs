using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Transactions;
using BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Processors
{
    public interface IPayStackProcessor
    {
        Task<PayloadResponse<VerifyAccountNumberResponseDTO>> VerifyAccountNumberAsync(string accounNumber, string bankCode);
        Task<PayloadResponse<IEnumerable<BanksData>>> GetBanksAsync();
    }
}
