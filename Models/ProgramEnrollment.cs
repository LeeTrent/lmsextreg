using System;
using System.ComponentModel.DataAnnotations;
using lmsextreg.Data;

namespace lmsextreg.Models
{
    public class ProgramEnrollment
    {
        ////////////////////////////////////////////////////////////
        // ProgramEnrollmentID:
        // Primary-key (auto-generated)
        ////////////////////////////////////////////////////////////
        public int ProgramEnrollmentID { get; set; 
        }
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
        [Display(Name = "Program")]
        public LMSProgram LMSProgram { get; set; }

        /////////////////////////////////////////////////////////////
        // LearnerUserId:
        // Same value as the 'Id' column in the 'AspNetUser' table
        // Foreign-key reference to ApplicationUser for Learner
        ////////////////////////////////////////////////////////////
        [Required]
        [Display(Name = "Student")]
        public string LearnerUserId { get; set; }

        //////////////////////////////////////////////////////////////
        // Program:
        // Navigation property to ApplicationUser entity for Learner
        /////////////////////////////////////////////////////////////
        public ApplicationUser Learner { get; set; }

        ////////////////////////////////////////////////////////////
        // ApproverUserId:
        // Same value as the 'Id' column in the 'AspNetUser' table
        ///////////////////////////////////////////////////////////
        [Display(Name = "Enrollment Approver")]
        public string ApproverUserId { get; set; }

        [Required]
        public string StatusCode { get; set; }

        [Display(Name = "Status")]
        public EnrollmentStatus EnrollmentStatus { get; set; }

        ////////////////////////////////////////////////////////////
        // UserCreated:
        // User who inserted this row
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        [Required]
        [Display(Name = "Created By")]
        public string UserCreated { get; set; }

        ////////////////////////////////////////////////////////////
        // DateCreated:
        // Date that row was originally inserted
        ///////////////////////////////////////////////////////////
        [Required]
        [Display(Name = "Date Requested")]
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