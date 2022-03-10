using E_Library.Models.Books;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Areas.Admin.Controllers
{
    public class BooksController : AdminController
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        [Area(AdminConstants.AreaName)]
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
    }
}
