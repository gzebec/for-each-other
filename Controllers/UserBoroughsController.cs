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
    public class UserBoroughsController : Controller
    {
        private readonly ApplicationContext _context;

        public UserBoroughsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: UserBoroughs
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.UserBoroughs.Include(u => u.Borough).Include(u => u.User);
            return View(await applicationContext.ToListAsync());
        }

        // GET: UserBoroughs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBorough = await _context.UserBoroughs
                .Include(u => u.Borough)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBorough == null)
            {
                return NotFound();
            }

            return View(userBorough);
        }

        // GET: UserBoroughs/Create
        public IActionResult Create()
        {
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text");
            return View();
        }

        // POST: UserBoroughs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BoroughId,Status,Created,CreatedBy,Updated,UpdatedBy")] UserBorough userBorough)
        {
            if (ModelState.IsValid)
            {
                userBorough.Created = DateTime.Now;
                userBorough.Updated = DateTime.Now;
                userBorough.CreatedBy = User.Identity.Name;
                userBorough.UpdatedBy = User.Identity.Name;
                _context.Add(userBorough);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Users", new { id = userBorough.UserId });
            }
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name", userBorough.BoroughId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", userBorough.UserId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", userBorough.Status);
            return View(userBorough);
        }

        // GET: UserBoroughs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBorough = await _context.UserBoroughs.FindAsync(id);
            if (userBorough == null)
            {
                return NotFound();
            }
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name", userBorough.BoroughId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", userBorough.UserId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", userBorough.Status);
            return View(userBorough);
        }

        // POST: UserBoroughs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,BoroughId,Status,Created,CreatedBy,Updated,UpdatedBy")] UserBorough userBorough)
        {
            if (id != userBorough.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userBorough.Updated = DateTime.Now;
                    userBorough.UpdatedBy = User.Identity.Name;
                    _context.Update(userBorough);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBoroughExists(userBorough.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Users", new { id = userBorough.UserId });
            }
            ViewData["BoroughId"] = new SelectList(_context.Boroughs, "Id", "Name", userBorough.BoroughId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", userBorough.UserId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", userBorough.Status);
            return View(userBorough);
        }

        // GET: UserBoroughs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBorough = await _context.UserBoroughs
                .Include(u => u.Borough)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBorough == null)
            {
                return NotFound();
            }

            return View(userBorough);
        }

        // POST: UserBoroughs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userBorough = await _context.UserBoroughs.FindAsync(id);
            _context.UserBoroughs.Remove(userBorough);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Details", "Users", new { id = userBorough.UserId });
        }

        private bool UserBoroughExists(int id)
        {
            return _context.UserBoroughs.Any(e => e.Id == id);
        }
    }
}
