
using System.ComponentModel.DataAnnotations;

namespace BaarakuMiniBankAPIs.Middleware.Core.Models
{
    public class Customer : BaseModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
