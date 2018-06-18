namespace lmsextreg.Constants
{
    public class TransitionLabelConstants
    {

        public static readonly string NONE_TO_PENDING       = "Initial enrollment request";
        public static readonly string PENDING_TO_WITHDRAWN  = "Applicant withdrew enrollment request";
        public static readonly string PENDING_TO_APPROVED   = "Enrollment request has been approved";
        public static readonly string PENDING_TO_DENIED     = "Enrollment request has been denied";
        public static readonly string APPROVED_TO_REVOKED   = "Program enrollment has been revoked";
        public static readonly string DENIED_TO_APPROVED    = "Enrollment request has been approved";
    }
}