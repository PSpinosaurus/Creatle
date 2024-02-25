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

namespace PresentationLayer
{
    public class GamesController : Controller
    {
        private readonly GameManager _gameManager;
        private readonly HeroProfileManager _heroProfileManager;
        private readonly AnswerManager _answerManager;

        public GamesController(GameManager gameManager, HeroProfileManager heroProfileManager, AnswerManager answerManager)
        {
            _gameManager = gameManager;
            _heroProfileManager = heroProfileManager;
            _answerManager = answerManager;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
              return View(await _gameManager.ReadAllAsync(true));
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _gameManager.ReadAsync((int)id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Games/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigationalProperties();
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Colour")] Game game)
        {
            if (ModelState.IsValid)
            {
                await _gameManager.CreateAsync(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameManager.ReadAsync((int)id);
            if (game == null)
            {
                return NotFound();
            }

            await LoadNavigationalProperties();
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Colour")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gameManager.UpdateAsync(game, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GameExists(game.Id))
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
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameManager.ReadAsync((int)id, true, true);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gameManager.DeleteAsync((int)id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadNavigationalProperties()
        {
            ViewData["Answers"] = new SelectList(await _answerManager.ReadAllAsync());
            ViewData["HeroProfiles"] = new SelectList(await _heroProfileManager.ReadAllAsync());
        }

        private async Task<bool> GameExists(int id)
        {
          return await _gameManager.ReadAsync(id) != null;
        }
    }
}
