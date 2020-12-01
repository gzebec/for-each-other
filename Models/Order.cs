using System;
using System.Collections.Generic;

namespace BPUIO_OneForEachOther.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BoroughId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PaymentType { get; set; }
        public string Note { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string GdprConsent { get; set; }
        public DateTime GdprConsentDate { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Borough Borough { get; set; }
    }
}