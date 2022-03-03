using E_Library.Data;
using E_Library.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services
{
    public class BookService
    {
        private readonly LibraryDbContext data;
        public BookService(LibraryDbContext data)
        {
            this.data = data;
        }
        public IEnumerable<BookCategoryViewModel> GetBookCategories()
        {
            return this.data
                .Categories
                .Select(c => new BookCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
        }
    }
}
