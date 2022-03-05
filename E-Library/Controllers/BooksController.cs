using E_Library.Models.Books;
using Microsoft.AspNetCore.Mvc;
using E_Library.Services.Contracts;

namespace E_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult All(string selectedCategory, string searchTerm)
        {
            var categories = this.bookService.GetBookCategories();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var soughtBooks = this.bookService
                    .FindBooks(searchTerm)
                    .Select(b => new BookListingViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        Price = b.Price,
                        ImageUrl = b.ImageUrl,
                        Release = b.Release,
                        Author = b.Author,
                        Category = b.Category
                    });
                if (!string.IsNullOrWhiteSpace(selectedCategory))
                {
                    soughtBooks = soughtBooks.Where(b => b.Category == selectedCategory);
                }
                return View(new AllBooksQueryModel
                {
                    AllBooks = soughtBooks,
                    SearchTerm = searchTerm,
                    SelectedCategory = selectedCategory,
                    Categories = categories
                });
            }
            else
            {

                var books = this.bookService
                    .GetBooks()
                    .Select(b => new BookListingViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        Price = b.Price,
                        ImageUrl = b.ImageUrl,
                        Release = b.Release,
                        Author = b.Author,
                        Category = b.Category
                    })
                    .ToList();
                if (!string.IsNullOrWhiteSpace(selectedCategory))
                {
                    books = books.Where(b => b.Category == selectedCategory).ToList();
                }
                return View(new AllBooksQueryModel
                {
                    AllBooks = books,
                    SearchTerm = searchTerm,
                    SelectedCategory = selectedCategory,
                    Categories = categories
                });
            }
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
