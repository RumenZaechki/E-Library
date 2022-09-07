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

        public async Task<IActionResult> All([FromQuery] AllBooksQueryModel query)
        {
            var categories = await this.bookService
                .GetBookCategoriesAsync();
            var resCat = categories
                .Select(c => new BookCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                });
            var books = await this.bookService
                .FindBooksAsync(query.SearchTerm, query.SelectedCategory, query.CurrentPage, AllBooksQueryModel.BooksPerPage);
            var resBooks = books
                .Select(b => new BookListingViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    Release = b.Release,
                    Author = b.Author,
                });
            return View(new AllBooksQueryModel
            {
                AllBooks = resBooks,
                SearchTerm = query.SearchTerm,
                SelectedCategory = query.SelectedCategory,
                Categories = resCat,
                BooksCount = await this.bookService.GetBooksCountAsync(query.SearchTerm, query.SelectedCategory),
                CurrentPage = query.CurrentPage,
            });
        }

        public async Task<IActionResult> Details(string id)
        {
            var book = await this.bookService.DetailsAsync(id);
            if (book == null)
            {
                return NotFound();
            }
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
                PublisherId = book.PublisherId,
                Publisher = book.Publisher,
                Category = book.Category
            });
        }
    }
}
