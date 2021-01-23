using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPUIO_OneForEachOther.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using BPUIO_OneForEachOther.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using BPUIO_OneForEachOther.Authorize;
using Microsoft.EntityFrameworkCore;

namespace BPUIO_OneForEachOther.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Find and validate the user:
            User user = GetUserByUsername(username);

            if (user == null || user.Password != Hash(password))
            {
                ModelState.AddModelError("", "Incorrect username or password.");
                return View();
            }

            // Create the identity
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            // Add roles
            /*foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }*/

            // Sign in
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // GET: Users/Create
        public IActionResult Register()
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
        public async Task<IActionResult> Register([Bind("Id,CountryId,AuthenticationSchemeId,Username,Password,PasswordRepeat,FirstName,LastName,Email,Address,Phone,Lat,Lng,GdprConsent,GdprConsentDate,Status,Created,CreatedBy,Updated,UpdatedBy")] User user)
        {
            // Find and validate the user:
            User? usernameCheck = GetUserByUsername(user.Username);
            
            // username check
            if (usernameCheck != null)
            {
                if (usernameCheck.Username.ToLower() == user.Username.ToLower())
                {
                    ModelState.AddModelError("", "User with username " + user.Username + " already exists.");
                }
            }

            // email check
            User? emailCheck = GetUserByEmail(user.Email);
            if (emailCheck != null)
            {
                if (emailCheck.Email.ToLower() == user.Email.ToLower())
                {
                    ModelState.AddModelError("", "User with email " + user.Email + " already exists.");
                }
            }

            // check password
            if (user.Password != user.PasswordRepeat)
            {
                ModelState.AddModelError("PasswordRepeat", "Passwords don't match.");
            }

            if (ModelState.IsValid)
            {
                user.Password = Hash(user.Password);
                user.Created = DateTime.Now;
                user.Updated = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Login", "Account");
            }
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name", user.AuthenticationSchemeId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", user.CountryId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", user.Status);
            return View(user);
        }

        [CustomAuthorize]
        // GET: Account/Profile/5
        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.AuthenticationScheme)
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.Username.ToLower() == id.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Account/Edit/5
        [CustomAuthorize]
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

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize]
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
                //return RedirectToAction(nameof(Profile));
                return RedirectToAction("Profile", "Account", new { id = user.Username });
            }
            ViewData["AuthenticationSchemeId"] = new SelectList(_context.AuthenticationSchemes, "Id", "Name", user.AuthenticationSchemeId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", user.CountryId);
            ViewData["Status"] = new SelectList(Utils.Extensions.GetRecordStatusList(), "Value", "Text", user.Status);
            return View(user);
        }

        // GET: Account/ChangePassword/5
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

        // POST: Account/ChangePassword/5
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
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Logout));
            }
            return View(user);
        }
        // ovdje završava change password

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private User GetUserByUsername(string username)
        {
            if (username == null)
            {
                return null;// NotFound();
            }

            var user = _context.Users.FirstOrDefault(m => m.Username.ToLower() == username.ToLower());

            if (user == null)
            {
                return null;// NotFound();
            }

            return user;
        }

        private User GetUserByEmail(string email)
        {
            if (email == null)
            {
                return null;// NotFound();
            }

            var user = _context.Users.FirstOrDefault(m => m.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return null;// NotFound();
            }

            return user;
        }

        public static string Hash(string password)
        {
            using (var sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
            }
        }
    }
}