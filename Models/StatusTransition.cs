using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class StatusTransition
    {
        [Required]
        public string FromStatusCode { get; set; }
        [Required]
        public string ToStatusCode { get; set; }
        [Required]
        public string ActionCode { get; set; }
        [Required]        
        public string ActionLabel { get; set; }
    }
}