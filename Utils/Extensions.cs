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
                new SelectListItem() { Text="Inactive", Value="Inactive"}
            };
            return recordStatus;
        }

        public static IEnumerable<SelectListItem> GetOrderStatusList()
        {
            IList<SelectListItem> orderStatus = new List<SelectListItem>
            {
                new SelectListItem() { Text="Worksheet", Value="Worksheet"},
                new SelectListItem() { Text="In progress", Value="In progress"},
                new SelectListItem() { Text="Finished", Value="Finished"},
                new SelectListItem() { Text="Canceled", Value="Canceled"}
            };
            return orderStatus;
        }
    }
}