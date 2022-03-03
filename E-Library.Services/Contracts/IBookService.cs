using E_Library.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services.Contracts
{
    public interface IBookService
    {
        public IEnumerable<BookCategoryViewModel> GetBookCategories();
    }
}
