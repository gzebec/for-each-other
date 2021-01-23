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
    [CustomAuthorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationContext _context;

        public OrdersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Orders.Include(o => o.Borough);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Orders/Details/5
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
                order.CreatedBy = User.Identity.Name;
                order.UpdatedBy = User.Identity.Name;
                _context.Add(order);
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Orders", new { id = order.Id });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", order.UserId);
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name", order.BoroughId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetOrderStatusList(), "Value", "Text", order.Status);
            return View(order);
        }

        // GET: Orders/Edit/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Orders/ChangeStatus/5
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
    }
}
