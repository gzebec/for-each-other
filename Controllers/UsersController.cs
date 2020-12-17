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
    public class UsersController : Controller
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Users.Include(u => u.AuthenticationScheme).Include(u => u.Country);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.AuthenticationScheme)
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryId,AuthenticationSchemeId,Username,Password,FirstName,LastName,Email,Address,Phone,Lat,Lng,GdprConsent,GdprConsentDate,Status,Created,CreatedBy,Updated,UpdatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Created = DateTime.Now;
                user.Updated = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name", user.AuthenticationSchemeId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", user.CountryId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name", user.AuthenticationSchemeId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", user.CountryId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryId,AuthenticationSchemeId,Username,Password,FirstName,LastName,Email,Address,Phone,Lat,Lng,GdprConsent,GdprConsentDate,Status,Created,CreatedBy,Updated,UpdatedBy")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Updated = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name", user.AuthenticationSchemeId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", user.CountryId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.AuthenticationScheme)
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
