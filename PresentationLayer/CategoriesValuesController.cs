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
using System.Reflection.Metadata.Ecma335;

namespace PresentationLayer
{
    public class CategoriesValuesController : Controller
    {
        private readonly CategoriesValuesManager _categoriesValuesManager;
        private readonly CategoriesManager _categoriesManager;
        private readonly HeroProfileManager _heroProfileManager;
        private readonly AnswerManager _answerManager;

        public CategoriesValuesController(CategoriesValuesManager categoriesValuesManager, CategoriesManager categoriesManager, HeroProfileManager heroProfileManager, AnswerManager answerManager)
        {
            _categoriesValuesManager = categoriesValuesManager;
            _categoriesManager = categoriesManager;
            _heroProfileManager = heroProfileManager;
            _answerManager = answerManager;
        }

        // GET: CategoriesValues
        public async Task<IActionResult> Index()
        {
            return View(await _categoriesValuesManager.ReadAllAsync(true));
        }

        // GET: CategoriesValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _categoriesValuesManager.ReadAsync((int)id);
            if (answer == null)
            {
                return NotFound();
            }
            await LoadNavigationalProperties();
            return View(answer);
        }

        // GET: CategoriesValues/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigationalProperties();
            return View();
        }

        // POST: CategoriesValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,CategoryId")] CategoriesValues categoriesValues)
        {
            categoriesValues.Category = await _categoriesManager.ReadAsync(categoriesValues.CategoryId);

            if (categoriesValues.Category != null)
            {
                await _categoriesValuesManager.CreateAsync(categoriesValues);
                return RedirectToAction(nameof(Index));
            }

            await LoadNavigationalProperties();
            return View(categoriesValues);
        }

        // GET: CategoriesValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _categoriesValuesManager.ReadAsync((int)id, true, true);
            if (answer == null)
            {
                return NotFound();
            }

            await LoadNavigationalProperties();
            return View(answer);
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
            categoriesValues.Category = await _categoriesManager.ReadAsync(categoriesValues.CategoryId);
            if (categoriesValues.Category != null)
            {
                try
                {
                    await _categoriesValuesManager.UpdateAsync(categoriesValues, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoriesValuesExists(categoriesValues.Id))
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
            return View(categoriesValues);
        }

        // GET: CategoriesValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriesValues = await _categoriesValuesManager.ReadAsync((int)id, true, true);
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
            await _categoriesValuesManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoriesValuesExists(int id)
        {
            return await _categoriesValuesManager.ReadAsync(id) != null;
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["Category"] = new SelectList(await _categoriesManager.ReadAllAsync(), "Id", "Name");
            ViewData["HeroProfiles"] = new SelectList(await _heroProfileManager.ReadAllAsync(), "HeroId", "Hero");
            ViewData["Answers"] = new SelectList(await _answerManager.ReadAllAsync(), "GameId", "Game");
        }
    }
}
