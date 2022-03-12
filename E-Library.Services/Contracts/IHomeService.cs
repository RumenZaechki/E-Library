using E_Library.Services.Books.Models;

namespace E_Library.Services.Contracts
{
    public interface IHomeService
    {
        public IEnumerable<BookServiceModel> GetBooks();
        public int GetTotalUsers();
        public int GetTotalBooks();
    }
}
