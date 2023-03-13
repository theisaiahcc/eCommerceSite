using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly GameContext _context;
        public MembersController(GameContext gameContext)
        {
            _context = gameContext;
        }
        [HttpGet] // get is default
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regMember) 
        {
            if (ModelState.IsValid)
            {
                // Map registerModel to Member Object
                Member newMember = new()
                {
                    Email = regMember.Email,
                    Password = regMember.Password,
                };
                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
