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
    public class BoroughsController : Controller
    {
        private readonly ApplicationContext _context;

        public BoroughsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Boroughs
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Boroughs.Include(b => b.City);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Boroughs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borough = await _context.Boroughs
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borough == null)
            {
                return NotFound();
            }

            return View(borough);
        }

        // GET: Boroughs/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Boroughs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,Code,Name,Status,Created,CreatedBy,Updated,UpdatedBy")] Borough borough)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borough);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", borough.CityId);
            return View(borough);
        }

        // GET: Boroughs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borough = await _context.Boroughs.FindAsync(id);
            if (borough == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", borough.CityId);
            return View(borough);
        }

        // POST: Boroughs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,Code,Name,Status,Created,CreatedBy,Updated,UpdatedBy")] Borough borough)
        {
            if (id != borough.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borough);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoroughExists(borough.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", borough.CityId);
            return View(borough);
        }

        // GET: Boroughs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borough = await _context.Boroughs
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borough == null)
            {
                return NotFound();
            }

            return View(borough);
        }

        // POST: Boroughs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borough = await _context.Boroughs.FindAsync(id);
            _context.Boroughs.Remove(borough);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoroughExists(int id)
        {
            return _context.Boroughs.Any(e => e.Id == id);
        }
    }
}
