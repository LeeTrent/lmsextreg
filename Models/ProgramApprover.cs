using System.ComponentModel.DataAnnotations;
namespace lmsextreg.Models
{
    public class ProgramApprover
    {
        [Required]
        public int LMSProgramID { get; set; }

        ////////////////////////////////////////////////////////////
        // ApproverUserId:
        // Same value as the 'Id' column in the 'AspNetUser' table
        ///////////////////////////////////////////////////////////
        [Required]
        public string ApproverUserId { get; set; }
    }
}