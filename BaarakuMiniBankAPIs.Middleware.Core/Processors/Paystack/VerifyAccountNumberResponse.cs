using Newtonsoft.Json;

namespace BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack
{
    public class VerifyAccountNumberResponse : BaseResponse
    {
        [JsonProperty("data")]
        public VerifyAccountData Data { get; set; }
    }

    public class VerifyAccountData
    {
        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty("account_name")]
        public string AccountName { get; set; }
        [JsonProperty("bank_id")]
        public string BankId { get; set; }
    }
}
