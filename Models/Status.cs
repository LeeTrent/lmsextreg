
using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class Status
    {
        [Key]
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}