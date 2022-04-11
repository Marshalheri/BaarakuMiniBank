
namespace BaarakuMiniBankAPIs.Middleware.Core.DTOs
{
    public class BaseRequestValidatorDTO
    {
        public virtual bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;
            return true;
        }
    }
}
