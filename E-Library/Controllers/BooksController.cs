using E_Library.Data;
using E_Library.Services;
using E_Library.Data.Models;
using E_Library.Models.Books;
using Microsoft.AspNetCore.Mvc;
using E_Library.Services.Contracts;

namespace E_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext data;
        private readonly IBookService bookService;
        public BooksController(LibraryDbContext data, IBookService bookService)
        {
            this.data = data;
            this.bookService = bookService;
        }
        public IActionResult Create() => View(new CreateBookFormModel
        {
            Categories = GetCategories()
        });
        [HttpPost]
        public IActionResult Create(CreateBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = GetCategories();
                return View(book);
            }
            bookService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.CategoryId);
            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<BookCategoryViewModel> GetCategories()
        {
            var categoriesObj = this.bookService.GetBookCategories();
            return categoriesObj.Select(c => new BookCategoryViewModel
            {
                Id = c.Key,
                Name = c.Value,
            });
        }
    }
}
