using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataLayer;
using ServiceLayer;
using System.ComponentModel;

namespace PresentationLayer
{
    public class HeroProfilesController : Controller
    {
        private readonly HeroProfileManager _manager;
        private readonly CategoriesValuesManager _categoriesValuesManager; // add this for the nav properties
        private readonly CategoriesManager _categoriesManager;
        private readonly GameManager _gameManager;
        private readonly HeroMetadataManager _heroMetadataManager;

        public HeroProfilesController(HeroProfileManager manager, 
            CategoriesValuesManager categoriesValuesManager, 
            CategoriesManager categoriesManager,
            GameManager gameManager, 
            HeroMetadataManager heroMetadataManager)
        {
            _manager = manager;
            _categoriesValuesManager = categoriesValuesManager;
            _categoriesManager = categoriesManager;
            _gameManager = gameManager;
            _heroMetadataManager = heroMetadataManager;
        }

        // GET: HeroProfiles
        public async Task<IActionResult> Index()
        {
            await LoadNavigationalProperties();
            return View(await _manager.ReadAllAsync(true));
        }

        // GET: HeroProfiles/Details/5
        [HttpGet("[action]/{ValueId}/{GameId}/{HeroId}/{CategoryId}")]
        public async Task<IActionResult> Details(int ValueId, int GameId, int HeroId, int CategoryId)
        {
            if (ValueId == null || GameId == null || HeroId == null || CategoryId == null)
            {
                return NotFound();
            }
            await LoadNavigationalProperties();
            object[] key = new object[] { ValueId, GameId, HeroId, CategoryId }; // only for composite key
            var heroProfile = await _manager.ReadAsync(key, true); // change this here
            if (heroProfile == null)
            {
                return NotFound();
            }

            return View(heroProfile);
        }

        // GET: HeroProfiles/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigationalProperties();
            return View();
        }

        // POST: HeroProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,CategoryId,HeroId,ValueId")] HeroProfile heroProfile)
        {
            heroProfile.Value = await _categoriesValuesManager.ReadAsync(heroProfile.ValueId); // add the navigational properties since you cannot bind them

            if (heroProfile.Value != null)
            {
                await _manager.CreateAsync(heroProfile);
                return RedirectToAction(nameof(Index));
            }
            await LoadNavigationalProperties();
            return View(heroProfile);
        }

        // GET: HeroProfiles/Edit/5
        [HttpGet("[action]/{ValueId}/{GameId}/{HeroId}/{CategoryId}")]
        public async Task<IActionResult> Edit(int ValueId, int GameId, int HeroId, int CategoryId)
        {
            if (ValueId == null || GameId == null || HeroId == null || CategoryId == null)
            {
                return NotFound();
            }

            object[] key = new object[] { ValueId, GameId, HeroId, CategoryId }; // only for composite key
            var heroProfile = await _manager.ReadAsync(key, true); // change this here
            if (heroProfile == null)
            {
                return NotFound();
            }
            await LoadNavigationalProperties();
            return View(heroProfile);
        }

        // POST: HeroProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[action]/{ValueId}/{GameId}/{HeroId}/{CategoryId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ValueId, int GameId, int HeroId, int CategoryId, [Bind("GameId,CategoryId,HeroId,ValueId")] HeroProfile heroProfile)
        {
            object[] key = new object[] { ValueId, GameId, HeroId, CategoryId }; // only for composite key

            if (ValueId != heroProfile.ValueId || GameId != heroProfile.GameId || HeroId != heroProfile.HeroId || CategoryId != heroProfile.CategoryId)
            {
                return NotFound();
            }
            heroProfile.Value = await _categoriesValuesManager.ReadAsync(heroProfile.ValueId); // add the navigational properties since you cannot bind them

            if (heroProfile.Value != null)
            {
                try
                {
                    await _manager.UpdateAsync(heroProfile, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await HeroProfileExists(key))
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
            return View(heroProfile);
        }

        // GET: HeroProfiles/Delete/5
        [HttpGet("[action]/{ValueId}/{GameId}/{HeroId}/{CategoryId}")]
        public async Task<IActionResult> Delete(int ValueId, int GameId, int HeroId, int CategoryId)
        {
            if (ValueId == null || GameId == null || HeroId == null || CategoryId == null)
            {
                return NotFound();
            }

            object[] key = new object[] { ValueId, GameId, HeroId, CategoryId }; // only for composite key
            var heroProfile = await _manager.ReadAsync(key, true); // change this here
            if (heroProfile == null)
            {
                return NotFound();
            }

            return View(heroProfile);
        }

        // POST: HeroProfiles/Delete/5
        [HttpPost("[action]/{ValueId}/{GameId}/{HeroId}/{CategoryId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ValueId, int GameId, int HeroId, int CategoryId)
        {
            object[] key = new object[] { ValueId, GameId, HeroId, CategoryId };
            await _manager.DeleteAsync(key);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> HeroProfileExists(object[] key)
        {
            return await _manager.ReadAsync(key) != null;
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["CategoryValueId"] = new SelectList(await _categoriesValuesManager.ReadAllAsync(), "Id", "Value");
            ViewData["GameId"] = new SelectList(await _gameManager.ReadAllAsync(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(await _categoriesManager.ReadAllAsync(), "Id", "Name");
            ViewData["HeroMetadataId"] = new SelectList(await _heroMetadataManager.ReadAllAsync(), "Id", "Name");

        }
    }
}
