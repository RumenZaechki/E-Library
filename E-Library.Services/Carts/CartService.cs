using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly LibraryDbContext data;
        public CartService(LibraryDbContext data)
        {
            this.data = data;
        }
        public async Task<bool> IsBookInCartAsync(string userId, string bookId)
        {
            var book = await this.data.Books.Where(x => x.Id == bookId).FirstOrDefaultAsync();
            var user = await this.data.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var cart = await this.data.Carts.Where(x => x.User == user).FirstOrDefaultAsync();
            if (this.data.BookCarts.Any(bc => bc.CartId == cart.Id && bc.BookId == book.Id))
            {
                return true;
            }
            return false;
        }
        public async Task BuyAsync(string userId)
        {
            var cart = await this.data.Carts.Where(x => x.User.Id == userId).FirstOrDefaultAsync();
            var bookCarts = this.data.BookCarts.Where(bc => bc.Cart == cart);
            if (cart != null && bookCarts != null)
            {
                foreach (var item in bookCarts)
                {
                    this.data.BookCarts.Remove(item);
                }
                await this.data.SaveChangesAsync();
            }
        }
        public async Task RemoveBookFromCartAsync(string bookId, string userId)
        {
            var cart = await this.data.Carts.Where(x => x.User.Id == userId).FirstOrDefaultAsync();
            var book = await this.data.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            var bookCart = await this.data.BookCarts.Where(bc => bc.Cart == cart).FirstOrDefaultAsync();
            if (cart != null && bookCart != null && book != null)
            {
                this.data.BookCarts.Remove(bookCart);
                await this.data.SaveChangesAsync();
            }
        }
        public async Task AddBookToCartAsync(string userId, string bookId)
        {
            var book = await this.data.Books.Where(x => x.Id == bookId).FirstOrDefaultAsync();
            var user = await this.data.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var cart = await this.data.Carts.Where(x => x.User == user).FirstOrDefaultAsync();
            if (book != null && user != null && cart != null)
            {
                this.data.BookCarts.Add(new BookCart
                {
                    BookId = bookId,
                    Book = book,
                    CartId = cart.Id,
                    Cart = cart
                });
                await this.data.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<CartBookModel>> GetBooksFromCartAsync(string userId)
        {
            var cart = await this.data.Carts.Where(x => x.User.Id == userId).FirstOrDefaultAsync();
            var bookCarts = this.data.BookCarts.Where(bc => bc.Cart == cart);
            return await bookCarts
                .Select(x => new CartBookModel
                {
                    Id = x.Book.Id,
                    Title = x.Book.Title,
                    Price = x.Book.Price.ToString("F2")
                })
                .ToListAsync();
        }
    }
}