
using E_Library.Services.Carts;

namespace E_Library.Services.Contracts
{
    public interface ICartService
    {
        public void AddBookToCart(string userId, string bookId);
        public IEnumerable<CartBookModel> GetBooksFromCart(string userId);
    }
}
