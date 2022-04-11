using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Customers;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core
{
    public interface ICustomerService
    {
        Task<PayloadResponse<CreateCustomerResponseDTO>> CreateCustomer(CreateCustomerRequestDTO request);
    }
}
