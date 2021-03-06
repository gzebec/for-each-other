﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPUIO_OneForEachOther.Data;
using BPUIO_OneForEachOther.Models;
using System.Security.Cryptography;
using System.Text;
using BPUIO_OneForEachOther.Authorize;

namespace BPUIO_OneForEachOther.Controllers
{
    [CustomAuthorize]
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
                .Include(r => r.UserRoles).ThenInclude(r => r.Role)
                .Include(r => r.UserBoroughs).ThenInclude(r => r.Borough)
                .Include(r => r.UserNotifications)
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
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text");
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
                user.Password = Hash(user.Password);
                user.Created = DateTime.Now;
                user.Updated = DateTime.Now;
                user.CreatedBy = User.Identity.Name;
                user.UpdatedBy = User.Identity.Name;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name", user.AuthenticationSchemeId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", user.CountryId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", user.Status);
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
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", user.Status);
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
                    user.UpdatedBy = User.Identity.Name;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!UserExists(user.Id))
                    {
                        ModelState.AddModelError("", "Not Found");
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name", user.AuthenticationSchemeId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", user.CountryId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", user.Status);
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

        // ovdje ide change password

        // GET: Users/Edit/5
        public async Task<IActionResult> ChangePassword(int? id)
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
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, string password, string passwordConfirm)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id); ;

            if (id != user.Id)
            {
                return NotFound();
            }

            if (Hash(password) != Hash(passwordConfirm))
            {
                return NotFound("Passwords don't match.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = Hash(password);
                    user.Updated = DateTime.Now;
                    user.UpdatedBy = User.Identity.Name;
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
            return View(user);
        }
        // ovdje završava change password

        public static string Hash (string password)
        {
            using (var sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
            }
        }
    }
}
