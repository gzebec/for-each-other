using BPUIO_OneForEachOther.Authorize;
using BPUIO_OneForEachOther.Data;
using BPUIO_OneForEachOther.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BPUIO_OneForEachOther.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        /*
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        */


        public class LocationsData
        {
            public int id { get; set; }
            public string title { get; set; }
            public string address { get; set; }
            public string description { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
            public string icon { get; set; }
            public string url { get; set; }
        }

        public IActionResult Index()
        {
            string markers = "[";
            markers += "{";
            markers += string.Format("'title': '{0}',", "Title");
            markers += string.Format("'lat': '{0}',", "45.8150");
            markers += string.Format("'lng': '{0}',", "15.9819");
            markers += string.Format("'description': '{0}'", "<a href=\"#\">Description</a>");
            markers += "},";

            markers += "];";
            ViewBag.Locations = GetLocations();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Maps    
        public ActionResult ShowMap()
        {
            return View();
        }

        #region   
        [HttpPost]
        public string GetLocations()
        {
            var usersData = UsersLocations();
            var ordersData = OrdersLocations();

            string markers = "[";

            foreach (var location in usersData)
            {
                if (location.lat != null && location.lng != null)
                markers += "{";
                markers += string.Format("'title': '{0}',", location.title); 
                markers += string.Format("'lat': '{0}',", location.lat);
                markers += string.Format("'lng': '{0}',", location.lng);
                markers += string.Format("'description': '{0}',", location.description);
                markers += string.Format("'icon': '{0}'", location.icon);
                markers += "},";
            }
            foreach (var location in ordersData)
            {
                if (location.lat != null && location.lng != null)
                    markers += "{";
                markers += string.Format("'title': '{0}',", location.title);
                markers += string.Format("'lat': '{0}',", location.lat);
                markers += string.Format("'lng': '{0}',", location.lng);
                markers += string.Format("'description': '{0}',", location.description);
                markers += string.Format("'icon': '{0}'", location.icon);
                markers += "},";
            }
            markers += "];";

            //return Json(data, new Newtonsoft.Json.JsonSerializerSettings()).ToString();
            //return JsonSerializer.Serialize<LocationsData>(data);
            /*
            var json = JsonSerializer.Serialize(data);
            Console.WriteLine("");
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings()).ToString();*/
            return markers;
        }

        public IEnumerable<LocationsData> UsersLocations()
        {
            return (from u in _context.Users where (u.Lat != null && u.Lng != null)
                    select new
                    {
                        title = u.FullName,
                        lat = u.Lat,
                        lng = u.Lng,
                        location = u.Address,
                        description = u.FullName + "<br>" + u.Address + " " + "<a class=\"fa fa-search\" href=\"Users/Details/?id=" + u.Id + "\"></a>",
                        icon = "https://maps.google.com/mapfiles/ms/icons/green-dot.png"
                    }).ToList()
                .Select(res => new LocationsData
                {
                    title = res.title,
                    lat = res.lat.ToString().Replace(",","."),
                    lng = res.lng.ToString().Replace(",", "."),
                    address = res.location,
                    description = res.description,
                    icon = res.icon
                });

        }

        public IEnumerable<LocationsData> OrdersLocations()
        {
            return (from u in _context.Orders where (u.Lat != null && u.Lng != null)
                    select new
                    {
                        title = u.FirstName + " " + u.LastName,
                        lat = u.Lat,
                        lng = u.Lng,
                        location = u.Address,
                        description = u.FirstName + " " + u.LastName + " " + u.Address + "<br>" + "<a class=\"fa fa-search\" href=\"Orders/Details/?id=" + u.Id + "\"></a>",
                        icon = "https://maps.google.com/mapfiles/ms/icons/red-dot.png"
                    }).ToList()
                .Select(res => new LocationsData
                {
                    title = res.title,
                    lat = res.lat.ToString().Replace(",", "."),
                    lng = res.lng.ToString().Replace(",", "."),
                    address = res.location,
                    description = res.description,
                    icon = res.icon
                });

        }

        #endregion

        public IEnumerable<LocationsData> Locations2()
        {
            return (from u in _context.Users where(u.Lat != null && u.Lng != null)
                    select new
                    {
                        title = u.FirstName + " " + u.LastName,
                        lat = u.Lat,
                        lng = u.Lng,
                        location = u.Address,
                        description = u.Address
                    }).ToList()
                .Select(res => new LocationsData
                {
                    title = res.title,
                    lat = res.lat.ToString().Replace(".", ","),
                    lng = res.lng.ToString().Replace(".", ","),
                    address = res.location,
                    description = res.description
                });

        }
    }
}
