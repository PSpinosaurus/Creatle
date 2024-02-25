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
    public class AnswersController : Controller
    {
        private readonly AnswerManager _answerManager; // you change this to use service layer
        private readonly CategoriesValuesManager _categoriesValuesManager; // add this for the nav properties
        private readonly CategoriesManager _categoriesManager;
        private readonly GameManager _gameManager;

        public AnswersController(AnswerManager manager, 
                                 CategoriesValuesManager categoriesValuesManager, 
                                 CategoriesManager categoriesManager, 
                                 GameManager gameManager)  // dont forget add scoped
        {
            _answerManager = manager;
            _categoriesValuesManager = categoriesValuesManager;
            _categoriesManager = categoriesManager;
            _gameManager = gameManager;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            return View(await _answerManager.ReadAllAsync(true)); // change here  go in the view and change the links at the bottom to use asp action 
        }

        // GET: Answers/Details/5
        [HttpGet("{Date}/d/{CategoryId}/{GameId}")]
        public async Task<IActionResult> Details(DateTime? date, int? CategoryId, int? GameId) // this is for composite pk 
        {
            if (date == null || CategoryId == null || GameId == null)
            {
                return NotFound();
            }

            object[] key = new object[] {date, CategoryId, GameId}; // only for composite key
            var answer = await _answerManager.ReadAsync(key); // change this here
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public async Task<IActionResult> Create() //dont forget to make methods async Task...
        {
            await LoadNavigationalProperties(); // add this method
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,GameId,CategoryId,CategoryValueId")] Answer answer)
        {
            answer.CategoryValue = await _categoriesValuesManager.ReadAsync(answer.CategoryValueId); // add the navigational properties since you cannot bind them

            if (ModelState.IsValid)
            {
                await _answerManager.CreateAsync(answer); // change here
                return RedirectToAction(nameof(Index)); 
            }

            await LoadNavigationalProperties();
            return View(answer);
        }

        // GET: Answers/Edit/5
        [HttpGet("{Date}/ed/{CategoryId}/{GameId}")]
        public async Task<IActionResult> Edit(DateTime? date, int? CategoryId, int? GameId)
        {
            if (date == null || CategoryId == null || GameId == null) // for composite 
            {
                return NotFound();
            }

            object[] key = new object[] { date, CategoryId, GameId }; // only for composite key
            var answer = await _answerManager.ReadAsync(key, true, false);
            if (answer == null)
            {
                return NotFound();
            }
            await LoadNavigationalProperties();
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{Date}/{CategoryId}/{GameId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime? date, int? CategoryId, int? GameId, [Bind("Date,GameId,CategoryId,CategoryValueId")] Answer answer)
        {
            object[] key = new object[] { date, CategoryId, GameId }; // only for composite key

            if (answer.Date != (DateTime)key[0] || answer.CategoryId != (int)key[1] || answer.GameId != (int)key[2])
            {
                return NotFound();
            }

            answer.CategoryValue = await _categoriesValuesManager.ReadAsync(answer.CategoryValueId); // add the navigational properties since you cannot bind them

            if (ModelState.IsValid)
            {
                try
                {
                    await _answerManager.UpdateAsync(answer, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AnswerExists(key))
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
            return View(answer);
        }

        // GET: Answers/Delete/5
        [HttpGet("{Date}/del/{CategoryId}/{GameId}")]
        public async Task<IActionResult> Delete(DateTime? date, int? CategoryId, int? GameId)
        {
            if (date == null || CategoryId == null || GameId == null) // for composite 
            {
                return NotFound();
            }

            object[] key = new object[] { date, CategoryId, GameId }; // only for composite key
            var answer = await _answerManager.ReadAsync(key, true, false);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost("{Date}/{CategoryId}/{GameId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime? date, int? CategoryId, int? GameId)
        {
            object[] key = new object[] { date, CategoryId, GameId }; // only for composite key
            await _answerManager.DeleteAsync(key);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["CategoryValueId"] = new SelectList(await _categoriesValuesManager.ReadAllAsync(), "Id", "Value");
            ViewData["GameId"] = new SelectList(await _categoriesManager.ReadAllAsync(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(await _gameManager.ReadAllAsync(), "Id", "Name");

        }

        private async Task<bool> AnswerExists(object[] key)
        {
            return await _answerManager.ReadAsync(key) != null;
        }
    }
}
