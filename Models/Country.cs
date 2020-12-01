using System;
using System.Collections.Generic;

namespace BPUIO_OneForEachOther.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string IconUrl { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<User> Users { get; set; }
    }
}