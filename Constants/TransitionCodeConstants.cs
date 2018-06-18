namespace lmsextreg.Constants
{
    public class TransitionCodeConstants
    {
        public static readonly string NONE_TO_PENDING       = "NONE_TO_PENDING";
        public static readonly string PENDING_TO_WITHDRAWN  = "PENDING_TO_WITHDRAWN";
        public static readonly string PENDING_TO_APPROVED   = "PENDING_TO_APPROVED";
        public static readonly string PENDING_TO_DENIED     = "PENDING_TO_DENIED";   
        public static readonly string APPROVED_TO_REVOKED   = "APPROVED_TO_REVOKED";
        public static readonly string DENIED_TO_APPROVED    = "DENIED_TO_APPROVED";
    }
}