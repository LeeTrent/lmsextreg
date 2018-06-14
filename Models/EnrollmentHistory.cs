using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class EnrollmentHistory
    {
        ////////////////////////////////////////////////////////////
        // EnrollmenHistoryID:
        // Primary Key
        ///////////////////////////////////////////////////////////
        [Required]
        public int EnrollmenHistoryID { get; set; }
        
        ////////////////////////////////////////////////////////////
        // ProgramEnrollmentID:
        // Foreign-key reference to ProgramEnrollment table
        ///////////////////////////////////////////////////////////
        [Required]
        public int ProgramEnrollmentID { get; set; }
      
        ////////////////////////////////////////////////////////////
        // StatusTransitionID:
        // Foreign-key reference to StatusTransition table
        ///////////////////////////////////////////////////////////      
        [Required]
        public int StatusTransitionID { get; set; }

        /////////////////////////////////////////////////////////////
        // ActorUserId:
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        [Required]        
        public string ActorUserId { get; set; }

        public string ActorRemarks { get; set; }
    }
}