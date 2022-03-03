using E_Library.Data;
using E_Library.Models.Books;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext data;
        public BooksController(LibraryDbContext data)
        {
            this.data = data;
        }
        public IActionResult Create() => View(new CreateBookFormModel
        {
            Categories = GetBookCategories()
        });
        [HttpPost]
        public IActionResult Create(CreateBookFormModel book)
        {
            return View();
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
