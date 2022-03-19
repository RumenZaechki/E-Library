using E_Library.Models.Books;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static E_Library.WebConstants;


namespace E_Library.Areas.Admin.Controllers
{
    public class BooksController : AdminController
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public IActionResult Create()
        {
            return View(new BookFormModel
            {
                Categories = GetCategories()
            });
        }

        [HttpPost]
        public IActionResult Create(BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = GetCategories();
                return View(book);
            }
            bookService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.AuthorDescription, book.AuthorImage, book.CategoryId);
            this.TempData[GlobalMessageKey] = "Successfully created book.";
            return RedirectToAction("Index", "Home", new {area = ""});
        }
        [Authorize]
        public IActionResult Edit(string id)
        {
            var book = this.bookService.Details(id);
            return View(new BookFormModel
            {
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                Release = book.Release,
                Author = book.Author,
                AuthorDescription = book.AuthorDescription,
                AuthorImage = book.AuthorImage,
                Categories = this.bookService
                    .GetBookCategories()
                    .Select(b => new BookCategoryViewModel
                    {
                        Id = b.Key,
                        Name = b.Value
                    })
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(string id, BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = GetCategories();
                return View(book);
            }
            this.bookService.Edit(id, book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.AuthorDescription, book.AuthorImage, book.CategoryId);
            this.TempData[GlobalMessageKey] = "Successfully edited book.";
            return RedirectToAction("All", "Books", new {area = ""});
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