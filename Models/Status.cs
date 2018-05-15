
using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class Status
    {
        [Key]
        public char StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}