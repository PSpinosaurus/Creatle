using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataLayer;

namespace PresentationLayer
{
    public class CategoriesValuesController : Controller
    {
        private readonly CreatleDbContext _context;

        public CategoriesValuesController(CreatleDbContext context)
        {
            _context = context;
        }

        // GET: CategoriesValues
        public async Task<IActionResult> Index()
        {
            var creatleDbContext = _context.CategoriesValues.Include(c => c.Category);
            return View(await creatleDbContext.ToListAsync());
        }

        // GET: CategoriesValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoriesValues == null)
            {
                return NotFound();
            }

            var categoriesValues = await _context.CategoriesValues
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesValues == null)
            {
                return NotFound();
            }

            return View(categoriesValues);
        }

        // GET: CategoriesValues/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: CategoriesValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,CategoryId")] CategoriesValues categoriesValues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriesValues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesValues.CategoryId);
            return View(categoriesValues);
        }

        // GET: CategoriesValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoriesValues == null)
            {
                return NotFound();
            }

            var categoriesValues = await _context.CategoriesValues.FindAsync(id);
            if (categoriesValues == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesValues.CategoryId);
            return View(categoriesValues);
        }

        // POST: CategoriesValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,CategoryId")] CategoriesValues categoriesValues)
        {
            if (id != categoriesValues.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriesValues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesValuesExists(categoriesValues.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoriesValues.CategoryId);
            return View(categoriesValues);
        }

        // GET: CategoriesValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoriesValues == null)
            {
                return NotFound();
            }

            var categoriesValues = await _context.CategoriesValues
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriesValues == null)
            {
                return NotFound();
            }

            return View(categoriesValues);
        }

        // POST: CategoriesValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoriesValues == null)
            {
                return Problem("Entity set 'CreatleDbContext.CategoriesValues'  is null.");
            }
            var categoriesValues = await _context.CategoriesValues.FindAsync(id);
            if (categoriesValues != null)
            {
                _context.CategoriesValues.Remove(categoriesValues);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesValuesExists(int id)
        {
          return (_context.CategoriesValues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
