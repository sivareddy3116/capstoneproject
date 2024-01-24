using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogWebApp.Models;
using BlogApplication.Models;
using BlogWebApp.Data;

namespace BlogApplication.Controllers
{
    public class EmpInfoesController : Controller
    {
        private readonly BlogDbContext _context;

        public EmpInfoesController(BlogDbContext context)
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
        public async Task<IActionResult> Login(Emplogin model)
        {
            if (ModelState.IsValid)
            {
                // Check if the provided credentials are valid
                var user = await _context.EmpInfo
                    .FirstOrDefaultAsync(u => u.EmailId == model.EmailId && u.PassCode == model.PassCode);

                if (user != null)
                {
                    // Redirect to a dashboard or another page after successful login
                    return RedirectToAction("Index","BlogInfoes");
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
        // GET: EmpInfoes
        public async Task<IActionResult> Index()
        {
            return _context.EmpInfo != null ?
                        View(await _context.EmpInfo.ToListAsync()) :
                        Problem("Entity set 'BlogDbContext.EmpInfo'  is null.");
        }

        // GET: EmpInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (empInfo == null)
            {
                return NotFound();
            }

            return View(empInfo);
        }

        // GET: EmpInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailId,Name,DateOfJoining,PassCode")] EmpInfo empInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empInfo);
        }

        // GET: EmpInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo.FindAsync(id);
            if (empInfo == null)
            {
                return NotFound();
            }
            return View(empInfo);
        }

        // POST: EmpInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmailId,Name,DateOfJoining,PassCode")] EmpInfo empInfo)
        {
            if (id != empInfo.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpInfoExists(empInfo.EmailId))
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
            return View(empInfo);
        }

        // GET: EmpInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (empInfo == null)
            {
                return NotFound();
            }

            return View(empInfo);
        }

        // POST: EmpInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EmpInfo == null)
            {
                return Problem("Entity set 'BlogDbContext.EmpInfo'  is null.");
            }
            var empInfo = await _context.EmpInfo.FindAsync(id);
            if (empInfo != null)
            {
                _context.EmpInfo.Remove(empInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpInfoExists(string id)
        {
            return (_context.EmpInfo?.Any(e => e.EmailId == id)).GetValueOrDefault();
        }
    }
}