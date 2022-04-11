
namespace BaarakuMiniBankAPIs.Middleware.Core.DTOs
{
    public class BasicResponse
    {
        public FaultMode FaultType { get; set; }
        public bool IsSuccessful { get; set; }
        public ErrorResponse Error { get; set; }

        public BasicResponse()
        {
            IsSuccessful = false;
        }

        public BasicResponse(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
    }

    public class ErrorResponse
    {
        public string Description { get; set; }
        public string ErrorCode { get; set; }

        public static T Create<T>(FaultMode fault, string errorCode, string errorMessage) where T : BasicResponse, new()
        {
            var response = new T()
            {
                IsSuccessful = false,
                FaultType = fault,
                Error = new ErrorResponse
                {
                    Description = errorMessage,
                    ErrorCode = errorCode
                }
            };

            return response;
        }
    }

    public enum FaultMode
    {
        CLIENT_INVALID_ARGUMENT,
        SERVER,
        TIMEOUT,
        REQUESTED_ENTITY_NOT_FOUND,
        INVALID_OBJECT_STATE,
        UNAUTHORIZED,
        GATEWAY_ERROR,
        LIMIT_EXCEEDED
    }
}
