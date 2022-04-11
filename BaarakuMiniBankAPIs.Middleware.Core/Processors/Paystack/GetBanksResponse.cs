using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack
{
    public class GetBanksResponse : BaseResponse
    {
        public IEnumerable<BanksData> Data { get; set; }
    }

    public class BanksData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("country")]
        public string  Country{ get; set; }
    }
}
