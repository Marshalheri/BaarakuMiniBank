
namespace BaarakuMiniBankAPIs.Middleware.Core.DTOs.Transactions
{
    public class FundAccountRequestDTO : BaseRequestValidatorDTO
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }

        public override bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;
            if (string.IsNullOrEmpty(AccountNumber))
            {
                problemSource = "Account Number";
                return false;
            }
            if (Amount <= 0)
            {
                problemSource = "Amount";
                return false;
            }
            return true;
        }
    }
}
