using E_Library.Data;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace E_Library.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly LibraryDbContext data;
        public CartService(LibraryDbContext data)
        {
            this.data = data;
        }
        [Authorize]
        public void AddBookToCart(string userId, string bookId)
        {
            var book = this.data.Books.FirstOrDefault(x => x.Id == bookId);
            var user = this.data.Users.FirstOrDefault(x => x.Id == userId);
            var cart = this.data.Carts.FirstOrDefault(x => x.User == user);
            cart.Books.Add(book);

            this.data.SaveChanges();
        }
    }
}
