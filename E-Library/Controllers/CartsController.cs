using E_Library.Models.Carts;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static E_Library.WebConstants;


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
            var message = this.cartService.AddBookToCart(userId, bookId);
            if (message != null)
            {
                this.TempData[GlobalMessageKey] = message;
                return RedirectToAction("All", "Books");
            }
            else
            {
                this.TempData[GlobalMessageKey] = "Successfully added book to cart.";   
                return RedirectToAction("MyCart", "Carts");
            }
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
            this.TempData[GlobalMessageKey] = "Successfully removed book from cart.";
            return RedirectToAction("MyCart", "Carts");
        }
        public IActionResult Buy()
        {
            var userId = GetUserId();
            this.cartService.Buy(userId);
            this.TempData[GlobalMessageKey] = "Successfully bought books.";
            return RedirectToAction("MyCart", "Carts");
        }
        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
