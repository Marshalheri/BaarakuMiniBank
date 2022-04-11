using BaarakuMiniBankAPIs.Middleware.Client.Filters;
using BaarakuMiniBankAPIs.Middleware.Core;
using BaarakuMiniBankAPIs.Middleware.Core.DTOs.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Client.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class CustomerController : RootController
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        /// <summary>
        /// onboard customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("onboard")]
        [ProducesResponseType(typeof(CreateCustomerResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [TypeFilter(typeof(ValidateRequestBodyFilter<CreateCustomerRequestDTO>))]
        public async Task<IActionResult> OnboardCustomer([FromBody] CreateCustomerRequestDTO request)
        {
            var result = await _service.CreateCustomer(request);
            if (!result.IsSuccessful)
            {
                return CreateResponse(result.Error, result.FaultType);
            }
            return Ok(result.GetPayload());
        }
    }
}
