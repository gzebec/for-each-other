using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPUIO_OneForEachOther.Models
{
    public class UserBorough
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true, Order = 1)]
        public int UserId { get; set; }
        [Required]
        [Index(IsUnique = true, Order = 2)]
        public int BoroughId { get; set; }
        [Required]
        [StringLength(30)]
        public string Status { get; set; }
        public DateTime Created { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        
        public virtual User User { get; set; }
        public virtual Borough Borough { get; set; }
    }
}