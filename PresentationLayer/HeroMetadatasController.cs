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
    public class HeroMetadatasController : Controller
    {
        private readonly CreatleDbContext _context;

        public HeroMetadatasController(CreatleDbContext context)
        {
            _context = context;
        }

        // GET: HeroMetadatas
        public async Task<IActionResult> Index()
        {
              return _context.HeroMetadata != null ? 
                          View(await _context.HeroMetadata.ToListAsync()) :
                          Problem("Entity set 'CreatleDbContext.HeroMetadata'  is null.");
        }

        // GET: HeroMetadatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HeroMetadata == null)
            {
                return NotFound();
            }

            var heroMetadata = await _context.HeroMetadata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heroMetadata == null)
            {
                return NotFound();
            }

            return View(heroMetadata);
        }

        // GET: HeroMetadatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HeroMetadatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Avatar")] HeroMetadata heroMetadata)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heroMetadata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heroMetadata);
        }

        // GET: HeroMetadatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HeroMetadata == null)
            {
                return NotFound();
            }

            var heroMetadata = await _context.HeroMetadata.FindAsync(id);
            if (heroMetadata == null)
            {
                return NotFound();
            }
            return View(heroMetadata);
        }

        // POST: HeroMetadatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Avatar")] HeroMetadata heroMetadata)
        {
            if (id != heroMetadata.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heroMetadata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroMetadataExists(heroMetadata.Id))
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
            return View(heroMetadata);
        }

        // GET: HeroMetadatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HeroMetadata == null)
            {
                return NotFound();
            }

            var heroMetadata = await _context.HeroMetadata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heroMetadata == null)
            {
                return NotFound();
            }

            return View(heroMetadata);
        }

        // POST: HeroMetadatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HeroMetadata == null)
            {
                return Problem("Entity set 'CreatleDbContext.HeroMetadata'  is null.");
            }
            var heroMetadata = await _context.HeroMetadata.FindAsync(id);
            if (heroMetadata != null)
            {
                _context.HeroMetadata.Remove(heroMetadata);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroMetadataExists(int id)
        {
          return (_context.HeroMetadata?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
