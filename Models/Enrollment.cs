using System;

namespace lmsextreg.Models
{
    public class Enrollment
    {
        ////////////////////////////////////////////////////////////
        // ProgramID:
        // Foreign-key reference to Program table
        ///////////////////////////////////////////////////////////
        public int ProgramID { get; set; }

        ////////////////////////////////////////////////////////////
        // Program:
        // Navigation property to Program entity
        ///////////////////////////////////////////////////////////
        public Program Program { get; set; }

        /////////////////////////////////////////////////////////////
        // LearnerID:
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        public string LearnerID { get; set; }

        ////////////////////////////////////////////////////////////
        // ApproverID:
        // Same value as the 'Id' column in the 'AspNetUser' table
        ///////////////////////////////////////////////////////////
        public string ApproverID { get; set; }

        public Status Status { get; set; }

        ////////////////////////////////////////////////////////////
        // UserCreated:
        // User who inserted this row
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        public string UserCreated { get; set; }

        ////////////////////////////////////////////////////////////
        // DateCreated:
        // Date that row was originally inserted
        ///////////////////////////////////////////////////////////
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