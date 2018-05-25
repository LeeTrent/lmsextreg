using System;
using System.ComponentModel.DataAnnotations;

namespace lmsextreg.Models
{
    public class ProgramEnrollment
    {
        ////////////////////////////////////////////////////////////
        // LMSProgramID:
        // Foreign-key reference to Program table
        ///////////////////////////////////////////////////////////
       [Required]
        public int LMSProgramID { get; set; }

        ////////////////////////////////////////////////////////////
        // Program:
        // Navigation property to Program entity
        ///////////////////////////////////////////////////////////
        public LMSProgram LMSProgram { get; set; }

        /////////////////////////////////////////////////////////////
        // LearnerUserId:
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        [Required]
        public string LearnerUserId { get; set; }

        ////////////////////////////////////////////////////////////
        // ApproverUserId:
        // Same value as the 'Id' column in the 'AspNetUser' table
        ///////////////////////////////////////////////////////////
        public string ApproverUserId { get; set; }

        [Required]
        public string StatusCode { get; set; }

        public EnrollmentStatus EnrollmentStatus { get; set; }

        ////////////////////////////////////////////////////////////
        // UserCreated:
        // User who inserted this row
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        [Required]
        public string UserCreated { get; set; }

        ////////////////////////////////////////////////////////////
        // DateCreated:
        // Date that row was originally inserted
        ///////////////////////////////////////////////////////////
        [Required]
        public DateTime DateCreated { get; set; }

        ////////////////////////////////////////////////////////////
        // UserCreated:
        // User who last updated this row
        // (Same value as the 'Id' column in the 'AspNetUser' table)        
        ///////////////////////////////////////////////////////////
        public string UserLastUpdated { get; set; }        

        ////////////////////////////////////////////////////////////
        // DateCreated:
        // Date that row was last updated
        ///////////////////////////////////////////////////////////
        public DateTime DateLastUpdated { get; set; }
    }
}