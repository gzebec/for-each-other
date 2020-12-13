using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPUIO_OneForEachOther.Data;
using BPUIO_OneForEachOther.Models;

namespace BPUIO_OneForEachOther.Controllers
{
    public class UserNotificationsController : Controller
    {
        private readonly ApplicationContext _context;

        public UserNotificationsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: UserNotifications
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.UserNotifications.Include(u => u.User);
            return View(await applicationContext.ToListAsync());
        }

        // GET: UserNotifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotifications
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userNotification == null)
            {
                return NotFound();
            }

            return View(userNotification);
        }

        // GET: UserNotifications/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName" + " " + "LastName");
            return View();
        }

        // POST: UserNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Subject,Text,Status,Created,CreatedBy,Updated,UpdatedBy")] UserNotification userNotification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userNotification.UserId);
            return View(userNotification);
        }

        // GET: UserNotifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotifications.FindAsync(id);
            if (userNotification == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userNotification.UserId);
            return View(userNotification);
        }

        // POST: UserNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Subject,Text,Status,Created,CreatedBy,Updated,UpdatedBy")] UserNotification userNotification)
        {
            if (id != userNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserNotificationExists(userNotification.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userNotification.UserId);
            return View(userNotification);
        }

        // GET: UserNotifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotifications
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userNotification == null)
            {
                return NotFound();
            }

            return View(userNotification);
        }

        // POST: UserNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userNotification = await _context.UserNotifications.FindAsync(id);
            _context.UserNotifications.Remove(userNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserNotificationExists(int id)
        {
            return _context.UserNotifications.Any(e => e.Id == id);
        }
    }
}
