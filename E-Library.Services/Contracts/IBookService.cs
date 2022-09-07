using E_Library.Services.Books.Models;

namespace E_Library.Services.Contracts
{
    public interface IBookService
    {
        public Task<int> GetBooksCountAsync(string searchTerm, string selectedCategory);
        public Task<IEnumerable<CategoryServiceModel>> GetBookCategoriesAsync();
        public Task CreateAsync(string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId);
        public Task<IEnumerable<BookServiceModel>> FindBooksAsync(string searchTerm, string selectedCategory, int currentPage, int booksPerPage);
        public Task<BookServiceModel> DetailsAsync(string id);
        public Task EditAsync(string id, string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId);
        public Task DeleteAsync(string id);
    }
}
