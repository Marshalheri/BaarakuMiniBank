
namespace BaarakuMiniBankAPIs.Middleware.Core.DTOs.Customers
{
    public class CreateCustomerRequestDTO : BaseRequestValidatorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public ImageDTO Image { get; set; }

        public override bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;
            if (string.IsNullOrEmpty(FirstName))
            {
                problemSource = "First Name";
                return false;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                problemSource = "Last Name";
                return false;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                problemSource = "Phone Number";
                return false;
            }
            if (string.IsNullOrEmpty(EmailAddress))
            {
                problemSource = "Email Address";
                return false;
            }
            if (!Image.IsValid(out problemSource))
            {
                return false;
            }
            return true;
        }
    }

    public class ImageDTO
    {
        public string RawData { get; set; }
        public string Extension { get; set; }

        public bool IsValid(out string sourceModel)
        {
            sourceModel = string.Empty;
            if (string.IsNullOrEmpty(RawData))
            {
                sourceModel = "Data";
                return false;
            }
            if (string.IsNullOrEmpty(Extension))
            {
                sourceModel = "Extension";
                return false;
            }
            return true;
        }
    }
}
