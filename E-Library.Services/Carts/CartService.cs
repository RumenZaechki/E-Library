using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;

namespace E_Library.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly LibraryDbContext data;
        public CartService(LibraryDbContext data)
        {
            this.data = data;
        }
        public void Buy(string userId)
        {
            var cart = this.data.Carts.Where(x => x.User.Id == userId).FirstOrDefault();
            var bookCarts = this.data.BookCarts.Where(bc => bc.Cart == cart);
            foreach (var item in bookCarts)
            {
                this.data.BookCarts.Remove(item);
            }
            this.data.SaveChanges();
        }
        public void RemoveBookFromCart(string bookId, string userId)
        {
            var cart = this.data.Carts.Where(x => x.User.Id == userId).FirstOrDefault();
            var bookCart = this.data.BookCarts.Where(bc => bc.Cart == cart).FirstOrDefault();
            this.data.BookCarts.Remove(bookCart);
            this.data.SaveChanges();
        }
        public string AddBookToCart(string userId, string bookId)
        {
            var book = this.data.Books.Where(x => x.Id == bookId).FirstOrDefault();
            var user = this.data.Users.Where(u => u.Id == userId).FirstOrDefault();
            var cart = this.data.Carts.Where(x => x.User == user).FirstOrDefault();
            if (this.data.BookCarts.Any(bc => bc.CartId == cart.Id && bc.BookId == book.Id))
            {
                return "Item already added to cart!";
            }
            else
            {
                this.data.BookCarts.Add(new BookCart
                {
                    BookId = bookId,
                    Book = book,
                    CartId = cart.Id,
                    Cart = cart
                });
                this.data.SaveChanges();
                return null;
            }
        }
        public IEnumerable<CartBookModel> GetBooksFromCart(string userId)
        {
            var cart = this.data.Carts.Where(x => x.User.Id == userId).FirstOrDefault();
            var bookCarts = this.data.BookCarts.Where(bc => bc.Cart == cart);
            return bookCarts
                .Select(x => new CartBookModel
                {
                    Id = x.Book.Id,
                    Title = x.Book.Title,
                    Price = x.Book.Price.ToString("F2")
                })
                .ToList();
        }
    }
}