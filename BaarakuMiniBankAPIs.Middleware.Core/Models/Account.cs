
using System.ComponentModel.DataAnnotations;

namespace BaarakuMiniBankAPIs.Middleware.Core.Models
{
    public class Account : BaseModel
    {
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public long CustomerId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsCreditFrozen { get; set; }
        [Required]
        public bool IsDebitFrozen { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
