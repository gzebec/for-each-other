using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPUIO_OneForEachOther.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int AuthenticationSchemeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string GdprConsent { get; set; }
        public DateTime GdprConsentDate { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<UserBorough> UserBoroughs { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public Country Country { get; set; }
        public AuthenticationScheme AuthenticationScheme { get; set; }
    }
}