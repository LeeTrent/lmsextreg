namespace lmsextreg.Models
{
    public class Transition
    {
        public int TransitionID { get; set; }
        public char FromStatusCode { get; set; }
        public char ToStatusCode { get; set; }
        public string ActionCode { get; set; }
        public string ActionLabel { get; set; }
    }
}