using BaarakuMiniBankAPIs.Middleware.Client.Filters;
using BaarakuMiniBankAPIs.Middleware.Core;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Transactions;
using BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Client.Controllers
{
    [Route("api/v1/transaction")]
    [ApiController]
    public class TransactionController : RootController
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService service)
        {
            _service = service;
        }
        /// <summary>
        /// get list of banks
        /// </summary>
        /// <returns></returns>
        [HttpGet("banks")]
        [ProducesResponseType(typeof(IEnumerable<BanksData>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBanks()
        {
            var result = await _service.GetBanksAsync();
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }

        /// <summary>
        /// verify customer account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        [HttpGet("verify/account/{accountNumber}/{bankCode}")]
        [ProducesResponseType(typeof(VerifyAccountNumberResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyCustomerAccount([FromRoute] string accountNumber, [FromRoute] string bankCode)
        {
            var result = await _service.VerifyCustomerAccountAsync(accountNumber, bankCode);
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }
        
        /// <summary>
        /// fund customer account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("fund/account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [TypeFilter(typeof(ValidateRequestBodyFilter<FundAccountRequestDTO>))]
        public async Task<IActionResult> FundCustomerAccount([FromBody] FundAccountRequestDTO request)
        {
            var result = await _service.FundCustomerAccountAsync(request);
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok();
        }
    }
}
