using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class LMSProgram
    { 
        [Required]
        public int LMSProgramID { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public string LongName { get; set; }
        public ICollection<ProgramApprover> ProgramApprovers { get; set; }
    }
}