using BaarakuMiniBankAPIs.Middleware.Core.DTOs;
using BaarakuMiniBankAPIs.Middleware.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Client.Filters
{
    public class ValidateRequestBodyFilter<T> : IAsyncActionFilter where T : BaseRequestValidatorDTO
    {
        public T RequestDataType { get; set; }
        private const string REQUEST = "request";
        private readonly IMessageProvider _messageProvider;
        private readonly ILogger<ValidateRequestBodyFilter<T>> _logger;

        public ValidateRequestBodyFilter(IMessageProvider messageProvider, ILogger<ValidateRequestBodyFilter<T>> logger)
        {
            _messageProvider = messageProvider;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.TryGetValue(REQUEST, out var output))
            {
                context.Result = new ObjectResult(
                    new ErrorResponse
                    {
                        ErrorCode = ResponseCodes.NO_REQUEST_BODY,
                        Description = _messageProvider.GetMessage(ResponseCodes.NO_REQUEST_BODY)
                    })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return;
            }

            var request = output as T;
            _logger.LogInformation($"{typeof(T).Name} REQUEST ==> {Util.SerializeAsJson(request)}");
            if (!request.IsValid(out string problemSource))
            {
                context.Result = new ObjectResult(
                    new ErrorResponse
                    {
                        ErrorCode = ResponseCodes.INVALID_INPUT_PARAMETER,
                        Description = $"{_messageProvider.GetMessage(ResponseCodes.INVALID_INPUT_PARAMETER)} - {problemSource}"
                    })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return;
            }

            await next();
        }
    }
}
