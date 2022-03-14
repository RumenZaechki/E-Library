using E_Library.Data;
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
        public void AddBookToCart(string userId, string bookId)
        {
            var book = this.data.Books.FirstOrDefault(x => x.Id == bookId);
            var cart = this.data.Carts.FirstOrDefault(x => x.UserId == userId);
            cart.Books.Add(book);

            this.data.SaveChanges();
        }
        public IEnumerable<CartBookModel> GetBooksFromCart(string userId)
        {
            var cart = this.data.Carts.FirstOrDefault(x => x.UserId == userId);
            return cart.Books
                .Select(x => new CartBookModel
                {
                    Title = x.Title,
                    Price = x.Price.ToString("F2")
                })
                .ToList();
        }
    }
}