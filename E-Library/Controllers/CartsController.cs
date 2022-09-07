using E_Library.Areas.Identity;
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
        public async Task<IActionResult> AddToCart(string bookId)
        {
            var userId = GetUserId();

            if (await cartService.IsBookInCartAsync(userId, bookId))
            {
                this.TempData[GlobalMessageKey] = "Item already added to cart!";
                return RedirectToAction("All", "Books");
            }

            await this.cartService.AddBookToCartAsync(userId, bookId);

            this.TempData[GlobalMessageKey] = "Successfully added book to cart.";
            return RedirectToAction("MyCart", "Carts");
        }
        [Authorize(Roles = UserConstants.UserRoleName)]
        public async Task<IActionResult> MyCart()
        {
            var userId = GetUserId();
            var books = await this.cartService.GetBooksFromCartAsync(userId);
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
        public async Task<IActionResult> Remove(string bookId)
        {
            var userId = GetUserId();
            await this.cartService.RemoveBookFromCartAsync(bookId, userId);
            this.TempData[GlobalMessageKey] = "Successfully removed book from cart.";
            return RedirectToAction("MyCart", "Carts");
        }
        [Authorize]
        public async Task<IActionResult> Buy()
        {
            var userId = GetUserId();
            await this.cartService.BuyAsync(userId);
            this.TempData[GlobalMessageKey] = "Successfully bought books.";
            return RedirectToAction("MyCart", "Carts");
        }
        private string GetUserId()
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
