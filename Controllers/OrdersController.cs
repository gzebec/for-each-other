using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPUIO_OneForEachOther.Data;
using BPUIO_OneForEachOther.Models;
using BPUIO_OneForEachOther.Authorize;

namespace BPUIO_OneForEachOther.Controllers
{
    //[CustomAuthorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationContext _context;

        public OrdersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Orders
        [CustomAuthorize]
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Orders.Include(o => o.Borough);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Orders/Details/5
        [CustomAuthorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Borough)
                .Include(d => d.OrderDetails)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name");
            ViewData["Status"] = new SelectList(Utils.Extensions.GetOrderStatusList(), "Value", "Text");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BoroughId,FirstName,LastName,Email,Address,Phone,DeliveryDate,PaymentType,Note,Lat,Lng,GdprConsent,GdprConsentDate,Status,Created,CreatedBy,Updated,UpdatedBy")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Created = DateTime.Now;
                order.Updated = DateTime.Now;
                order.CreatedBy = "Anonymous";//User.Identity.Name;
                order.UpdatedBy = "Anonymous";//User.Identity.Name;
                _context.Add(order);
                await _context.SaveChangesAsync();

                AddStatusNotification(order);

                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Orders", new { id = order.Id });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", order.UserId);
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name", order.BoroughId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetOrderStatusList(), "Value", "Text", order.Status);
            return View(order);
        }

        // GET: Orders/Edit/5
        [CustomAuthorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", order.UserId);
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name", order.BoroughId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetOrderStatusList(), "Value", "Text", order.Status);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,BoroughId,FirstName,LastName,Email,Address,Phone,DeliveryDate,PaymentType,Note,Lat,Lng,GdprConsent,GdprConsentDate,Status,Created,CreatedBy,Updated,UpdatedBy")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.Updated = DateTime.Now;
                    order.UpdatedBy = User.Identity.Name;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", order.UserId);
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name", order.BoroughId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetOrderStatusList(), "Value", "Text", order.Status);
            return View(order);
        }

        // GET: Orders/Delete/5
        [CustomAuthorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Borough)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [CustomAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (_context.OrderDetails.Any(e => e.OrderId == id))
            {
                ModelState.AddModelError("", "Order contains items. Please delete them first.");
                return View(order);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Orders/ChangeStatus/5
        [CustomAuthorize]
        public async Task<IActionResult> ChangeStatus(int? id, string status, string username = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            int? userId = null;
            if (username != null)
            {
                userId = GetUserId(username);
            }

            if (!_context.OrderDetails.Any(e => e.OrderId == id))
            {
                return NotFound("Order must have at least one item.");
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (userId != null)
                    {
                        order.UserId = userId;
                    }

                    order.Status = status;
                    await _context.SaveChangesAsync();

                    AddStatusNotification(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Details", "Orders", new { id = order.Id });

            //return View(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private int GetUserId(string username)
        {
            var order = _context.Users.FirstOrDefault(e => e.Username.ToLower() == username.ToLower());

            return order.Id;
        }

        public string GetStatusColor (string status)
        {
            string color = "";
            if (status == "Worksheet")
            {
                color = "info";
            }
            else if (status == "In progress")
            {
                color = "warning";
            }
            else if (status == "Finished")
            {
                color = "success";
            }
            else if (status == "Canceled")
            {
                color = "secondary";
            }

            return color;
        }

        private void AddStatusNotification (Order order)
        {
            UserNotification userNotification = new UserNotification();
            int? userId = GetUserId(@User.Identity.Name);
            if (userId == null)
            {
                userId = 1;
            }

            if (order.Status == "Worksheet")
            {
                userNotification.Subject = string.Format("Order no. {0} created", order.Id);
                userNotification.Text = string.Format("Order created.<br><br>nOrder number: {0}<brName: {1} {2}<br>Address: {3}<br>Phone: {4}<br>Email: {5}<br>Delivery date: {6}<br>Paymend: {7}<br>Note: {8}", 
                order.Id, order.FirstName, order.LastName, order.Address, order.Phone, order.Email, order.DeliveryDate.ToString("dd.MM.yyyy"), order.PaymentType, order.Note);
            }
            else if (order.Status == "In progress")
            {
                userNotification.Subject = string.Format("Order no.{0} in progress", order.Id);
                userNotification.Text = string.Format("Order {0} is in progress.<br>Assigned to user {1}.", order.Id, order.User.FullName);
            }
            else if (order.Status == "Finished")
            {
                userNotification.Subject = string.Format("Order no. {0} finished", order.Id);
                userNotification.Text= string.Format("Order {0} is finished.<br>Assigned to user {1}.", order.Id, order.User.FullName);
            }
            else if (order.Status == "Canceled")
            {
                userNotification.Subject = string.Format("Order no. {0} canceled", order.Id);
                userNotification.Text = string.Format("Order {0} is canceled.<br>Assigned to user {1}.", order.Id, order.User.FullName);
            }

            userNotification.UserId = (int)userId;
            userNotification.Created = DateTime.Now;
            userNotification.Updated = DateTime.Now;
            userNotification.CreatedBy = User.Identity.Name;
            userNotification.UpdatedBy = User.Identity.Name;
            userNotification.Status= "Active";
            _context.Add(userNotification);
            _context.SaveChanges();

        }
    }
}
