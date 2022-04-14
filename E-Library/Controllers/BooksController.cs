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

        public IActionResult All([FromQuery] AllBooksQueryModel query)
        {
            var categories = this.bookService
                .GetBookCategories()
                .Select(c => new BookCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                });
            var books = this.bookService
                .FindBooks(query.SearchTerm, query.SelectedCategory, query.CurrentPage, AllBooksQueryModel.BooksPerPage)
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
                AllBooks = books,
                SearchTerm = query.SearchTerm,
                SelectedCategory = query.SelectedCategory,
                Categories = categories,
                BooksCount = this.bookService.GetBooksCount(query.SearchTerm, query.SelectedCategory),
                CurrentPage = query.CurrentPage,
            });
        }

        public IActionResult Details(string id)
        {
            var book = this.bookService.Details(id);
            if (book == null)
            {
                return RedirectToAction("Error", "Home", new
                {
                    Description = "Couldn't find book."
                });
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
