
using System.ComponentModel.DataAnnotations;

namespace BaarakuMiniBankAPIs.Middleware.Core.Models
{
    public class Image : BaseModel
    {
        [Required]
        public string RawData { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
