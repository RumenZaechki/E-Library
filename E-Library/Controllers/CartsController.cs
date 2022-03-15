using E_Library.Models.Carts;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult AddToCart(string bookId)
        {
            var userId = GetUserId();
            this.cartService.AddBookToCart(userId, bookId);
            return RedirectToAction("MyCart", "Carts");
        }
        public IActionResult MyCart()
        {
            var userId = GetUserId();
            var books = this.cartService.GetBooksFromCart(userId);
            var cartDetails = books
                .Select(b => new CartDetailsViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                })
               .ToList();
            return View(cartDetails);
        }
        public IActionResult Remove(string bookId)
        {
            var userId = GetUserId();
            this.cartService.RemoveBookFromCart(bookId, userId);
            return RedirectToAction("MyCart", "Carts");
        }
        public IActionResult Buy()
        {
            var userId = GetUserId();
            this.cartService.Buy(userId);
            return RedirectToAction("MyCart", "Carts");
        }
        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
