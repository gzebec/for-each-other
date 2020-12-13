using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPUIO_OneForEachOther.Models
{
    public class AuthenticationScheme
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Status { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<User> Users { get; set; }
    }
}