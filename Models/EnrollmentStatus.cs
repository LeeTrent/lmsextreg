
using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class EnrollmentStatus
    {
        [Key]
        [Required]
        public string StatusCode { get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}