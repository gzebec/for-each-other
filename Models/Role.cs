using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPUIO_OneForEachOther.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(30)]
        public string Status { get; set; }
        public DateTime Created { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}