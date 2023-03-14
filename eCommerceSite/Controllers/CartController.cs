using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly GameContext _context;
        private const string Cart = "ShoppingCart";

        public CartController(GameContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Game? gameToAdd = _context.Games.Where(g => g.Id == id).SingleOrDefault();

            if (gameToAdd == null)
            {
                TempData["ErrorMessage"] = "That game no longer exists";
                // game with id doesnt exist
                return RedirectToAction("Index", "Games");
            }
            CartGameViewModel cartGame = new()
            {
                Id = gameToAdd.Id,
                Title = gameToAdd.Title,
                Price = gameToAdd.Price
            };

            List<CartGameViewModel> cartGames = GetCartData();
            cartGames.Add(cartGame);

            WriteCartCookie(cartGames);
            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "Games");
        }

        /// <summary>
        /// Returns the current list of games in shopping cart.
        /// If no shopping cart cookie, returns empty list.
        /// </summary>
        private List<CartGameViewModel> GetCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];
            if (String.IsNullOrWhiteSpace(cookie))
            {
                return new List<CartGameViewModel>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<CartGameViewModel>>(cookie);
            }
        }

        public IActionResult Summary()
        {
            // Read shopping cart data and convert to list of view model
            List<CartGameViewModel> cartGames = GetCartData();
            return View(cartGames);
        }

        public IActionResult Remove(int Id)
        {
            List<CartGameViewModel> cartGames = GetCartData();
            CartGameViewModel? targetGame = cartGames.FirstOrDefault(g => g.Id == Id);
            cartGames.Remove(targetGame);
            WriteCartCookie(cartGames);
            return RedirectToAction(nameof(Summary));
        }

        /// <summary>
        /// Writes List of CartGameViewModels to Cookie
        /// </summary>
        private void WriteCartCookie(List<CartGameViewModel> cartGames)
        {
            HttpContext.Response.Cookies.Append(Cart, JsonConvert.SerializeObject(cartGames), new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
        }
    }
}
