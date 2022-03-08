using E_Library.Models.Books;
using Microsoft.AspNetCore.Mvc;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace E_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult All([FromQuery] AllBooksQueryModel query)
        {
            var categories = this.bookService.GetBookCategories();
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var soughtBooks = this.bookService
                    .FindBooks(query.SearchTerm)
                    .Skip((query.CurrentPage - 1) * AllBooksQueryModel.BooksPerPage)
                    .Take(AllBooksQueryModel.BooksPerPage)
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
                if (!string.IsNullOrWhiteSpace(query.SelectedCategory))
                {
                    soughtBooks = soughtBooks.Where(b => b.Category == query.SelectedCategory);
                }
                return View(new AllBooksQueryModel
                {
                    AllBooks = soughtBooks,
                    SearchTerm = query.SearchTerm,
                    SelectedCategory = query.SelectedCategory,
                    Categories = categories,
                });
            }
            else
            {
                var books = this.bookService
                    .GetBooks()
                    .Skip((query.CurrentPage - 1) * AllBooksQueryModel.BooksPerPage)
                    .Take(AllBooksQueryModel.BooksPerPage)
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
                if (!string.IsNullOrWhiteSpace(query.SelectedCategory))
                {
                    books = books.Where(b => b.Category == query.SelectedCategory).ToList();
                }
                return View(new AllBooksQueryModel
                {
                    AllBooks = books,
                    SearchTerm = query.SearchTerm,
                    SelectedCategory = query.SelectedCategory,
                    Categories = categories,
                });
            }
        }

        public IActionResult Create() => View(new BookFormModel
        {
            Categories = GetCategories()
        });

        [HttpPost]
        public IActionResult Create(BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = GetCategories();
                return View(book);
            }
            bookService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.CategoryId);
            return RedirectToAction("Index", "Home");
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
            this.bookService.Edit(id, book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.CategoryId);
            return RedirectToAction(nameof(All));
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
