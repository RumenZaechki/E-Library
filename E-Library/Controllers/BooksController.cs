using E_Library.Models.Books;
using Microsoft.AspNetCore.Mvc;
using E_Library.Services.Contracts;

using static E_Library.WebConstants;

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
            var books = this.bookService
                .FindBooks(query.SearchTerm, query.CurrentPage, AllBooksQueryModel.BooksPerPage)
                .Select(b => new BookListingViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    Release = b.Release,
                    Author = b.Author,
                });
            if (!string.IsNullOrWhiteSpace(query.SelectedCategory))
            {
                books = this.bookService
                    .FindBooksThatMatchCategory(query.SelectedCategory)
                    .Select(b => new BookListingViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        ImageUrl = b.ImageUrl,
                        Release = b.Release,
                        Author = b.Author,
                    });
            }
            return View(new AllBooksQueryModel
            {
                AllBooks = books,
                SearchTerm = query.SearchTerm,
                SelectedCategory = query.SelectedCategory,
                Categories = categories,
                BooksCount = this.bookService.GetBooksCount()
            });
        }

        public IActionResult Delete(string id)
        {
            this.bookService.Delete(id);
            this.TempData[GlobalMessageKey] = "Successfully removed book.";
            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(string id)
        {
            var book = this.bookService.Details(id);
            return View(new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price.ToString("F2"),
                ImageUrl = book.ImageUrl,
                Release = book.Release,
                AuthorId = book.AuthorId,
                Author = book.Author,
                Category = book.Category
            });
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
