using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BPUIO_OneForEachOther.Models
{
    public class UserNotification
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(255)]
        public string Subject { get; set; }
        [Required]
        [StringLength(4000)]
        public string Text { get; set; }
        [Required]
        [StringLength(30)]
        public string Status { get; set; }
        public DateTime Created { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public User User { get; set; }
    }
}