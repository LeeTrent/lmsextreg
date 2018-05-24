using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class StatusTransition
    {
        [Required]
        public EnrollmentStatus FromStatus { get; set; }
        [Required]
        public EnrollmentStatus ToStatus { get; set; }
        [Required]
        public string ActionCode { get; set; }
        [Required]        
        public string ActionLabel { get; set; }
    }
}