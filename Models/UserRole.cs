using System;
using System.Collections.Generic;

namespace BPUIO_OneForEachOther.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}