using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPUIO_OneForEachOther.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true, Order = 1)]
        public int RoleId { get; set; }
        [Required]
        [Index(IsUnique = true, Order = 2)]
        public int UserId { get; set; }
        [Required]
        [StringLength(30)]
        public string Status { get; set; }
        public DateTime Created { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}