using E_Library.Services.Carts;

namespace E_Library.Services.Contracts
{
    public interface ICartService
    {
        public void Buy(string userId);
        public void RemoveBookFromCart(string bookId, string userId);
        public string AddBookToCart(string userId, string bookId);
        public IEnumerable<CartBookModel> GetBooksFromCart(string userId);
    }
}
