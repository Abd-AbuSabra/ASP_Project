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
using ASP_Project.Models.ViewModels;


namespace ASP_Project.Controllers
{
    [Authorize]
    
    public class EBooksController : Controller
    {
        private readonly DataContext _context;

        public EBooksController(DataContext context)
        {
            _context = context;
        }

        // GET: EBooks
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> Index()
        {
            ViewData["Tax"] = 1.16;
            return View(await _context.EBook.OrderBy(b =>b.Title).ToListAsync());
        }
        public IActionResult Find()
        {
            return View(new FindBook());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Find([Bind("Title,BookPrice")] FindBook findBook)
        {
            //Select * From Books Where Title Like '%book.Title%'
            IEnumerable<EBook> result = Enumerable.Empty<EBook>();

            if (findBook != null && findBook!.Title != String.Empty)
            {
                result = await _context.EBook.Where(b => b.Title.Contains(findBook!.Title!)).ToListAsync();

                if (findBook != null && findBook!.BookPrice > 0)
                {
                    result = result.Where(b => b.BookPrice <= findBook!.BookPrice!).ToList();
                }
            }
            findBook!.EBooks = result;
            findBook!.EBooks = findBook.EBooks.OrderByDescending(b => b.BookPrice).ThenByDescending(b => b.Title).ToList();
            return View(findBook);
        }
        // GET: EBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Tax"] = 1.16;
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _context.EBook
                .FirstOrDefaultAsync(m => m.EBookId == id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // GET: EBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EBookId,Title,BookPrice")] EBook eBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eBook);
        }

        // GET: EBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _context.EBook.FindAsync(id);
            if (eBook == null)
            {
                return NotFound();
            }
            return View(eBook);
        }

        // POST: EBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EBookId,Title,BookPrice")] EBook eBook)
        {
            if (id != eBook.EBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EBookExists(eBook.EBookId))
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
            return View(eBook);
        }

        // GET: EBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Tax"] = 1.16;
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _context.EBook
                .FirstOrDefaultAsync(m => m.EBookId == id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // POST: EBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eBook = await _context.EBook.FindAsync(id);
            if (eBook != null)
            {
                _context.EBook.Remove(eBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EBookExists(int id)
        {
            return _context.EBook.Any(e => e.EBookId == id);
        }
    }
}
