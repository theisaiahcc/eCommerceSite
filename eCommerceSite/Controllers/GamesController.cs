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
    }
}
