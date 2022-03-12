using E_Library.Services.Books.Models;

namespace E_Library.Services.Contracts
{
    public interface IBookService
    {
        public int GetBooksCount();
        public Dictionary<int, string> GetBookCategories();
        public void Create(string title, string description, decimal price, string imageUrl, int release, string author, int categoryId);
        public IEnumerable<BookServiceModel> GetIndexBooks();
        public IEnumerable<BookServiceModel> GetBooks(int currentPage, int booksPerPage);
        public int GetTotalUsers();
        public int GetTotalBooks();
        public IEnumerable<BookServiceModel> FindBooks(string searchTerm, int currentPage, int booksPerPage);
        public BookServiceModel Details(string id);
        public void Edit(string id, string title, string description, decimal price, string imageUrl, int release, string author, int categoryId);
        public void Delete(string id);
    }
}
