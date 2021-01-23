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
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int AuthenticationSchemeId { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [NotMapped]
        [StringLength(50)]
        public string PasswordRepeat { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get {return FirstName + " " + LastName;} }
        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(50)]
        [Phone]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Lat { get; set; }
        [StringLength(20)]
        public string Lng { get; set; }
        [StringLength(1)]
        public string GdprConsent { get; set; }
        public DateTime? GdprConsentDate { get; set; }
        [Required]
        [StringLength(30)]
        public string Status { get; set; }
        public DateTime Created { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<UserBorough> UserBoroughs { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public Country Country { get; set; }
        public AuthenticationScheme AuthenticationScheme { get; set; }
    }
}