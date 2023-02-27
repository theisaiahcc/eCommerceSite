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
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                _context.Games.Add(game); // prepares insert
                _context.SaveChanges();   // executes insert
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
