using System.ComponentModel.DataAnnotations;
using lmsextreg.Data;

namespace lmsextreg.Models
{
    public class ProgramApprover
    {
        [Required]
        public int LMSProgramID { get; set; }

        //////////////////////////////////////////////////////////////
        // LMSProgram:
        // Navigation property to ApplicationUser entity for LMSProgram
        /////////////////////////////////////////////////////////////
        public LMSProgram LMSProgram { get; set; }

        ////////////////////////////////////////////////////////////
        // ApproverUserId:
        // Same value as the 'Id' column in the 'AspNetUser' table
        ///////////////////////////////////////////////////////////
        [Required]
        public string ApproverUserId { get; set; }

        //////////////////////////////////////////////////////////////
        // Approver:
        // Navigation property to ApplicationUser entity for APPROVER
        /////////////////////////////////////////////////////////////
        [Display(Name = "Approver")]
        public ApplicationUser Approver { get; set; }        
    }
}