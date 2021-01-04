using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BPUIO_OneForEachOther.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public int CountryId { get; set; }
        [PersonalData] 
        public int AuthenticationSchemeId { get; set; }
        [PersonalData] 
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string Address { get; set; }
        [PersonalData]
        public string Phone { get; set; }
        public decimal Lat { get; set; }
        [PersonalData] 
        public decimal Lng { get; set; }
        [PersonalData] 
        public string GdprConsent { get; set; }
        [PersonalData] 
        public DateTime GdprConsentDate { get; set; }
        [PersonalData] 
        public string Status { get; set; }
        [PersonalData] 
        public DateTime Created { get; set; }
        [PersonalData] 
        public string CreatedBy { get; set; }
        [PersonalData] 
        public DateTime Updated { get; set; }
        [PersonalData] 
        public string UpdatedBy { get; set; }
    }
}
