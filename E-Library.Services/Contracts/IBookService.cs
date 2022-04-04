using E_Library.Services.Books.Models;

namespace E_Library.Services.Contracts
{
    public interface IBookService
    {
        public int GetBooksCount(string searchTerm, string selectedCategory);
        public IEnumerable<CategoryServiceModel> GetBookCategories();
        public void Create(string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId);
        public IEnumerable<BookServiceModel> FindBooks(string searchTerm, string selectedCategory, int currentPage, int booksPerPage);
        public BookServiceModel Details(string id);
        public void Edit(string id, string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId);
        public void Delete(string id);
    }
}
