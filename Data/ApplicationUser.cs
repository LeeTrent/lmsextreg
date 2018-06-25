using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using lmsextreg.Models;

namespace lmsextreg.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string JobTitle { get; set; }   
        public string AgencyID { get; set; }
        public Agency Agency { get; set; }
        public string SubAgencyID { get; set; }
        public SubAgency SubAgency { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime DateExpired { get; set; }
        public bool RulesOfBehaviorAgreedTo { get; set; }

    }
}
