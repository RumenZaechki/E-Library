using E_Library.Services.Carts;

namespace E_Library.Services.Contracts
{
    public interface ICartService
    {
        public Task<bool> IsBookInCartAsync(string userId, string bookId);
        public Task BuyAsync(string userId);
        public Task RemoveBookFromCartAsync(string bookId, string userId);
        public Task AddBookToCartAsync(string userId, string bookId);
        public Task<IEnumerable<CartBookModel>> GetBooksFromCartAsync(string userId);
    }
}
