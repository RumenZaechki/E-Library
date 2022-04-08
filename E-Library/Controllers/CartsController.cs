using E_Library.Areas.Admin;
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

            if (cartService.IsBookInCart(userId, bookId))
            {
                this.TempData[GlobalMessageKey] = "Item already added to cart!";
                return RedirectToAction("All", "Books");
            }

            this.cartService.AddBookToCart(userId, bookId);

            this.TempData[GlobalMessageKey] = "Successfully added book to cart.";
            return RedirectToAction("MyCart", "Carts");
        }
        [Authorize]
        public IActionResult MyCart()
        {
            if (User.IsInRole(AdminConstants.AdminRoleName))
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }
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
        [Authorize]
        public IActionResult Remove(string bookId)
        {
            var userId = GetUserId();
            this.cartService.RemoveBookFromCart(bookId, userId);
            this.TempData[GlobalMessageKey] = "Successfully removed book from cart.";
            return RedirectToAction("MyCart", "Carts");
        }
        [Authorize]
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
