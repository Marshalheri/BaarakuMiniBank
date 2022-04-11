namespace BaarakuMiniBankAPIs.Middleware.Core.Processors.Paystack
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }

        public bool IsSuccessful()
        {
            return Status;
        }
    }
}
