namespace lmsextreg.Models
{
    public class History
    {
        public int HistoryID { get; set; }
        
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

        public int TransitionID { get; set; }
        public Transition Transition { get; set; }  

        /////////////////////////////////////////////////////////////
        // ActorID:
        // (Same value as the 'Id' column in the 'AspNetUser' table)
        ////////////////////////////////////////////////////////////
        public string ActorID { get; set; }

        public string ActorRemarks { get; set; }
    }
}