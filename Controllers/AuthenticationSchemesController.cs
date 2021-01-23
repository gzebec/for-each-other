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
    public class AuthenticationSchemesController : Controller
    {
        private readonly ApplicationContext _context;

        public AuthenticationSchemesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: AuthenticationSchemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuthenticationSchemes.ToListAsync());
        }

        // GET: AuthenticationSchemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authenticationScheme = await _context.AuthenticationSchemes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authenticationScheme == null)
            {
                return NotFound();
            }

            return View(authenticationScheme);
        }

        // GET: AuthenticationSchemes/Create
        public IActionResult Create()
        {
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text");
            return View();
        }

        // POST: AuthenticationSchemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Created,CreatedBy,Updated,UpdatedBy")] AuthenticationScheme authenticationScheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authenticationScheme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", authenticationScheme.Status);
            return View(authenticationScheme);
        }

        // GET: AuthenticationSchemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authenticationScheme = await _context.AuthenticationSchemes.FindAsync(id);
            if (authenticationScheme == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", authenticationScheme.Status);
            return View(authenticationScheme);
        }

        // POST: AuthenticationSchemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Created,CreatedBy,Updated,UpdatedBy")] AuthenticationScheme authenticationScheme)
        {
            if (id != authenticationScheme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authenticationScheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthenticationSchemeExists(authenticationScheme.Id))
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
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", authenticationScheme.Status);
            return View(authenticationScheme);
        }

        // GET: AuthenticationSchemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authenticationScheme = await _context.AuthenticationSchemes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authenticationScheme == null)
            {
                return NotFound();
            }

            return View(authenticationScheme);
        }

        // POST: AuthenticationSchemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authenticationScheme = await _context.AuthenticationSchemes.FindAsync(id);
            _context.AuthenticationSchemes.Remove(authenticationScheme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthenticationSchemeExists(int id)
        {
            return _context.AuthenticationSchemes.Any(e => e.Id == id);
        }
    }
}
