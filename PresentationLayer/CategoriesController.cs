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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PresentationLayer
{
    public class CategoriesController : Controller
    {
        private readonly CategoriesManager _categoriesManager;
        private readonly AnswerManager _answerManager;
        private readonly CategoriesValuesManager _categoriesValuesManager;
        private readonly HeroProfileManager _heroProfileManager;

        public CategoriesController(CategoriesManager manager, AnswerManager answerManager, CategoriesValuesManager categoriesValuesManager, HeroProfileManager heroProfileManager)
        {
            _categoriesManager = manager;
            _answerManager = answerManager;
            _categoriesValuesManager = categoriesValuesManager;
            _heroProfileManager = heroProfileManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return View(await _categoriesManager.ReadAllAsync(true));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _categoriesManager.ReadAsync((int)id); // change this here
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigationalProperties();
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                await _categoriesManager.CreateAsync(categories);
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesManager.ReadAsync((int)id);
            if (categories == null)
            {
                return NotFound();
            }

            await LoadNavigationalProperties();
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Categories categories)
        {
            if (id != categories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoriesManager.UpdateAsync(categories, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoriesExists(categories.Id))
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

            await LoadNavigationalProperties();
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesManager.ReadAsync((int)id, true, true);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriesManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["Answers"] = new SelectList(await _answerManager.ReadAllAsync());
            ViewData["CategoriesValues"] = new SelectList(await _categoriesValuesManager.ReadAllAsync());
            ViewData["HeroProfiles"] = new SelectList(await _heroProfileManager.ReadAllAsync());
        }

        private async Task<bool> CategoriesExists(int id)
        {
            return await _categoriesManager.ReadAsync(id) != null;
        }
    }
}
