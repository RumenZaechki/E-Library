using E_Library.Services.Books.Models;

namespace E_Library.Services.Contracts
{
    public interface IHomeService
    {
        public List<BookServiceModel> GetRecentBooks();
    }
}
