using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_Project.Data;
using ASP_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASP_Project.Controllers
{
    [Authorize]
  
    public class ELibrariesController : Controller
    {
        private readonly DataContext _context;

        public ELibrariesController(DataContext context)
        {
            _context = context;
        }

        // GET: ELibraries
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ELibrary.Include(e => e.eBook).Include(e => e.user);
            return View(await dataContext.ToListAsync());
        }

        // GET: ELibraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eLibrary = await _context.ELibrary
                .Include(e => e.eBook)
                .Include(e => e.user)
                .FirstOrDefaultAsync(m => m.ELibraryId == id);
            if (eLibrary == null)
            {
                return NotFound();
            }

            return View(eLibrary);
        }

        // GET: ELibraries/Create
        public IActionResult Create()
        {
            ViewData["EBookId"] = new SelectList(_context.EBook, "EBookId", "Title");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserName");
            return View();
        }

        // POST: ELibraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ELibraryId,UserId,EBookId,BookDescription,PublishingDate,Genera,IsAvailable")] ELibrary eLibrary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eLibrary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EBookId"] = new SelectList(_context.EBook, "EBookId", "Title", eLibrary.EBookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserName", eLibrary.UserId);
            return View(eLibrary);
        }

        // GET: ELibraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eLibrary = await _context.ELibrary.FindAsync(id);
            if (eLibrary == null)
            {
                return NotFound();
            }
            ViewData["EBookId"] = new SelectList(_context.EBook, "EBookId", "Title", eLibrary.EBookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserName", eLibrary.UserId);
            return View(eLibrary);
        }

        // POST: ELibraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ELibraryId,UserId,EBookId,BookDescription,PublishingDate,Genera,IsAvailable")] ELibrary eLibrary)
        {
            if (id != eLibrary.ELibraryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eLibrary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ELibraryExists(eLibrary.ELibraryId))
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
            ViewData["EBookId"] = new SelectList(_context.EBook, "EBookId", "Title", eLibrary.EBookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserName", eLibrary.UserId);
            return View(eLibrary);
        }

        // GET: ELibraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eLibrary = await _context.ELibrary
                .Include(e => e.eBook)
                .Include(e => e.user)
                .FirstOrDefaultAsync(m => m.ELibraryId == id);
            if (eLibrary == null)
            {
                return NotFound();
            }

            return View(eLibrary);
        }

        // POST: ELibraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eLibrary = await _context.ELibrary.FindAsync(id);
            if (eLibrary != null)
            {
                _context.ELibrary.Remove(eLibrary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ELibraryExists(int id)
        {
            return _context.ELibrary.Any(e => e.ELibraryId == id);
        }
    }
}
