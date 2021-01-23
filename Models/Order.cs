using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BPUIO_OneForEachOther.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [Required]
        public int BoroughId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(50)]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [StringLength(50)]
        public string PaymentType { get; set; }
        [StringLength(2000)]
        public string Note { get; set; }
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

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Borough Borough { get; set; }

        public User User { get; set; }
    }
}