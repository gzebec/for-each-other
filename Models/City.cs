using System;
using System.Collections.Generic;

namespace BPUIO_OneForEachOther.Models
{
    public class City
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<Borough> Boroughs { get; set; }
        public Country Country { get; set; }
    }
}