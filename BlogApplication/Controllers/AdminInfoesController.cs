using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogWebApp.Data;
using BlogWebApp.Models;
using Microsoft.AspNetCore.Identity;
using BlogApplication.Models;
using NuGet.Protocol.Plugins;

namespace BlogApplication.Controllers
{
    public class AdminInfoesController : Controller
    {
        private readonly BlogDbContext _context;
        

        public AdminInfoesController(BlogDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(login model)
        {
            if (ModelState.IsValid)
            {
                // Check if the provided credentials are valid
                var user = await _context.AdminInfo
                    .FirstOrDefaultAsync(u => u.EmailId == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Redirect to a dashboard or another page after successful login
                    return RedirectToAction("Index","EmpInfoes");
                }
                else
                {
                    // Set an error message for invalid login attempt
                    TempData["ErrorMessage"] = "Invalid login attempt";
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");

            }

            return View(model);
        }


        // GET: AdminInfoes
        public async Task<IActionResult> Index()
        {
              return _context.AdminInfo != null ? 
                          View(await _context.AdminInfo.ToListAsync()) :
                          Problem("Entity set 'BlogDbContext.AdminInfo'  is null.");
        }

        // GET: AdminInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AdminInfo == null)
            {
                return NotFound();
            }

            var adminInfo = await _context.AdminInfo
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (adminInfo == null)
            {
                return NotFound();
            }

            return View(adminInfo);
        }

        // GET: AdminInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailId,Password")] AdminInfo adminInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminInfo);
        }

        // GET: AdminInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AdminInfo == null)
            {
                return NotFound();
            }

            var adminInfo = await _context.AdminInfo.FindAsync(id);
            if (adminInfo == null)
            {
                return NotFound();
            }
            return View(adminInfo);
        }

        // POST: AdminInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmailId,Password")] AdminInfo adminInfo)
        {
            if (id != adminInfo.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminInfoExists(adminInfo.EmailId))
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
            return View(adminInfo);
        }

        // GET: AdminInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AdminInfo == null)
            {
                return NotFound();
            }

            var adminInfo = await _context.AdminInfo
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (adminInfo == null)
            {
                return NotFound();
            }

            return View(adminInfo);
        }

        // POST: AdminInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AdminInfo == null)
            {
                return Problem("Entity set 'BlogDbContext.AdminInfo'  is null.");
            }
            var adminInfo = await _context.AdminInfo.FindAsync(id);
            if (adminInfo != null)
            {
                _context.AdminInfo.Remove(adminInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminInfoExists(string id)
        {
          return (_context.AdminInfo?.Any(e => e.EmailId == id)).GetValueOrDefault();
        }
    }
}
