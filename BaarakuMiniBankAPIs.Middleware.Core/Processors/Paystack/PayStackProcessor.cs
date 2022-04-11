using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Transactions;
using BaarakuMiniBankAPIs.Middleware.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack
{
    public class PayStackProcessor : IPayStackProcessor
    {
        private readonly HttpClient _client;
        private readonly PaystackSettings _bankSettings;
        readonly ILogger _logger;
        internal const string _prefix = "PS";
        public PayStackProcessor(IOptions<PaystackSettings> bankProcessorSettings, IHttpClientFactory factory, ILogger<PayStackProcessor> logger)
        {
            _logger = logger;
            _bankSettings = bankProcessorSettings.Value;
            _client = factory.CreateClient("HttpMessageHandler");
            BuildFiClient();
        }
        private void BuildFiClient()
        {
            _client.BaseAddress = new Uri(_bankSettings.BaseUrl);
            _client.DefaultRequestHeaders.Add("Authorization", $"bearer {_bankSettings.Key}");
        }
        protected async Task<T> GetMessage<T>(string path)
        {
            var rawResponse = await _client.GetAsync(path);
            var body = await rawResponse.Content.ReadAsStringAsync();
            _logger.LogInformation($"{typeof(T).Name} Paystack Response : {body}");
            try
            {
                return Util.DeserializeFromJson<T>(body);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing Paysatck response");
                throw new Exception("Failed to deserialize Paysatck response.");
            }
        }
        public async Task<PayloadResponse<VerifyAccountNumberResponseDTO>> VerifyAccountNumberAsync(string accounNumber, string bankCode)
        {
            var response = new PayloadResponse<VerifyAccountNumberResponseDTO>(false);
            var serviceResponse = await GetMessage<VerifyAccountNumberResponse>($"bank/resolve?account_number={accounNumber}&bank_code={bankCode}");
            if (!serviceResponse.IsSuccessful())
            {
                response.Error = new ErrorResponse
                {
                    ErrorCode = $"{_prefix}01",
                    Description = serviceResponse.Message,
                };
                return response;
            }

            response.SetPayload(new VerifyAccountNumberResponseDTO { AccountName = serviceResponse.Data.AccountName });
            response.IsSuccessful = true;
            return response;
        }

        public async Task<PayloadResponse<IEnumerable<BanksData>>> GetBanksAsync()
        {
            var response = new PayloadResponse<IEnumerable<BanksData>>(false);
            var serviceResponse = await GetMessage<GetBanksResponse>($"bank");
            if (!serviceResponse.IsSuccessful())
            {
                response.Error = new ErrorResponse
                {
                    ErrorCode = $"{_prefix}02",
                    Description = serviceResponse.Message,
                };
                return response;
            }

            response.SetPayload(serviceResponse.Data.Select(x => new BanksData
            {
                Code = x.Code,
                Country = x.Country,
                Currency = x.Currency,
                Name = x.Name
            }));
            response.IsSuccessful = true;
            return response;
        }
    }
}
