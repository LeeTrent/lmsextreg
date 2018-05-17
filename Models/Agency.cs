using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace lmsextreg.Models
{
    public class Agency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AgencyID { get; set; }
        public string AgencyName { get; set; }
        public int DisplayOrder { get; set; }
        public string TreasuryCode{ get; set; }
        public string OPMCode{ get; set; }
        public ICollection<SubAgency> SubAgencies { get; set; }
    }

}