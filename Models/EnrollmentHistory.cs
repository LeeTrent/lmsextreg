using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class EnrollmentHistory
    {
        [Required]
        public int EnrollmenHistoryID { get; set; }
        
        ////////////////////////////////////////////////////////////
        // ProgramID:
        // Foreign-key reference to Program table
        ///////////////////////////////////////////////////////////
        [Required]
        public int ProgramID { get; set; }

        ////////////////////////////////////////////////////////////
        // Program:
        // Navigation property to Program entity
        ///////////////////////////////////////////////////////////
        public Program Program { get; set; }      

        [Required]
        public int StatusTransitionID { get; set; }
        public StatusTransition StatusTransition { get; set; }  

        /////////////////////////////////////////////////////////////
        // ActorUserId:
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        [Required]        
        public string ActorUserId { get; set; }

        public string ActorRemarks { get; set; }
    }
}