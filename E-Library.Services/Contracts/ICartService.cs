using E_Library.Services.Carts;

namespace E_Library.Services.Contracts
{
    public interface ICartService
    {
        public bool IsBookInCart(string userId, string bookId);
        public void Buy(string userId);
        public void RemoveBookFromCart(string bookId, string userId);
        public void AddBookToCart(string userId, string bookId);
        public IEnumerable<CartBookModel> GetBooksFromCart(string userId);
    }
}
