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

    }
}
