using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameContext _context;
        public GamesController(GameContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get all games from db
            List<Game> games = await _context.Games.ToListAsync();
            // Show them on page
            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game game) // async and wrap original return type in Task<>
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                _context.Games.Add(game); // prepares insert
                await _context.SaveChangesAsync();   // executes insert await keyword for async code
                // Show success message
                ViewData["Message"] = $"{game.Title} was added successfully.";
                return View();
            }
            return View(game);
        }
        /*public IActionResult Index()
        {
            return View();
        }*/

        public async Task<IActionResult> Edit(int id) 
        {
            Game? game = await _context.Games.FindAsync(id);
            if (game != null) 
            {
                return View(game);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Update(game);
                await _context.SaveChangesAsync();
            }
            TempData["Message"] = game.Title + " updated successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Game? game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                return View(game);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game? game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                TempData["Message"] = game.Title + " deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "This game has already been deleted!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Game? game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                return View(game);
            }
            return NotFound();
        }
    }
}
