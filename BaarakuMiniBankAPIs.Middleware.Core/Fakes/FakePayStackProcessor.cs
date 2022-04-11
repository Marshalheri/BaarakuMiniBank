using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Transactions;
using BaarakuMiniBankAPIs.Middleware.Core.Processors;
using BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Fakes
{
    public class FakePayStackProcessor : IPayStackProcessor
    {
        public Task<PayloadResponse<IEnumerable<BanksData>>> GetBanksAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PayloadResponse<VerifyAccountNumberResponseDTO>> VerifyAccountNumberAsync(string accounNumber, string bankCode)
        {
            var response = new PayloadResponse<VerifyAccountNumberResponseDTO>(true);
            response.SetPayload(new VerifyAccountNumberResponseDTO { AccountName = "John Doe" });
            return Task.FromResult(response);
        }
    }
}
