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
    public class HeroProfilesController : Controller
    {
        private readonly CreatleDbContext _context;

        public HeroProfilesController(CreatleDbContext context)
        {
            _context = context;
        }

        // GET: HeroProfiles
        public async Task<IActionResult> Index()
        {
            var creatleDbContext = _context.HeroProfiles.Include(h => h.Value);
            return View(await creatleDbContext.ToListAsync());
        }

        // GET: HeroProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HeroProfiles == null)
            {
                return NotFound();
            }

            var heroProfile = await _context.HeroProfiles
                .Include(h => h.Value)
                .FirstOrDefaultAsync(m => m.ValueId == id);
            if (heroProfile == null)
            {
                return NotFound();
            }

            return View(heroProfile);
        }

        // GET: HeroProfiles/Create
        public IActionResult Create()
        {
            ViewData["ValueId"] = new SelectList(_context.CategoriesValues, "Id", "Value");
            return View();
        }

        // POST: HeroProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,CategoryId,HeroId,ValueId")] HeroProfile heroProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heroProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ValueId"] = new SelectList(_context.CategoriesValues, "Id", "Value", heroProfile.ValueId);
            return View(heroProfile);
        }

        // GET: HeroProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HeroProfiles == null)
            {
                return NotFound();
            }

            var heroProfile = await _context.HeroProfiles.FindAsync(id);
            if (heroProfile == null)
            {
                return NotFound();
            }
            ViewData["ValueId"] = new SelectList(_context.CategoriesValues, "Id", "Value", heroProfile.ValueId);
            return View(heroProfile);
        }

        // POST: HeroProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,CategoryId,HeroId,ValueId")] HeroProfile heroProfile)
        {
            if (id != heroProfile.ValueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heroProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroProfileExists(heroProfile.ValueId))
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
            ViewData["ValueId"] = new SelectList(_context.CategoriesValues, "Id", "Value", heroProfile.ValueId);
            return View(heroProfile);
        }

        // GET: HeroProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HeroProfiles == null)
            {
                return NotFound();
            }

            var heroProfile = await _context.HeroProfiles
                .Include(h => h.Value)
                .FirstOrDefaultAsync(m => m.ValueId == id);
            if (heroProfile == null)
            {
                return NotFound();
            }

            return View(heroProfile);
        }

        // POST: HeroProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HeroProfiles == null)
            {
                return Problem("Entity set 'CreatleDbContext.HeroProfiles'  is null.");
            }
            var heroProfile = await _context.HeroProfiles.FindAsync(id);
            if (heroProfile != null)
            {
                _context.HeroProfiles.Remove(heroProfile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroProfileExists(int id)
        {
          return (_context.HeroProfiles?.Any(e => e.ValueId == id)).GetValueOrDefault();
        }
    }
}
