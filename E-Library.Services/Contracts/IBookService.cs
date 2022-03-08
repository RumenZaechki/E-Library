using E_Library.Services.Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services.Contracts
{
    public interface IBookService
    {
        public Dictionary<int, string> GetBookCategories();
        public void Create(string title, string description, decimal price, string imageUrl, int release, string author, int categoryId);
        public IEnumerable<BookServiceModel> GetBooks();
        public int GetTotalUsers();
        public int GetTotalBooks();
        public IEnumerable<BookServiceModel> FindBooks(string searchTerm);
        public BookServiceModel Details(string id);
        public void Edit(string id, string title, string description, decimal price, string imageUrl, int release, string author, int categoryId);
    }
}
