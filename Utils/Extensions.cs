using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPUIO_OneForEachOther.Utils
{
    public class Extensions
    {
        public static IEnumerable<SelectListItem> GetRecordStatusList()
        {
            IList<SelectListItem> recordStatus = new List<SelectListItem>
            {
                new SelectListItem() { Text="Active", Value="Active"},
                new SelectListItem() { Text="Inactive", Value="Inactive"},
            };
            return recordStatus;
        }
    }
}