using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Library.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService cartService;
        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public IActionResult AddToCart(string bookId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.cartService.AddBookToCart(userId, bookId);
            return RedirectToAction("All","Books");
        }
    }
}
