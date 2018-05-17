using System.ComponentModel.DataAnnotations.Schema;
namespace lmsextreg.Models
{
    public class SubAgency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubAgencyID { get; set; }
        public string AgencyID { get; set; }
        public Agency Agency { get; set; }
        public string SubAgencyName { get; set; }
        public int DisplayOrder { get; set; }
        public string TreasuryCode{ get; set; }
        public string OPMCode{ get; set; }     
    }
}