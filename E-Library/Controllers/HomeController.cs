using E_Library.Models;
using E_Library.Models.Books;
using E_Library.Models.Home;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService bookService;
        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            this.bookService = bookService;
        }

        public IActionResult Index()
        {
            var totalBooks = this.bookService.GetTotalBooks();
            var totalUsers = this.bookService.GetTotalUsers();
            var books = this.bookService.GetIndexBooks();
            var booksToShow = books.Select(b => new BookListingViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                Release = b.Release,
                Author = b.Author,
                Category = b.Category
            })
                .OrderByDescending(c => c.Id)
                .Take(3)
                .ToList();
            return View(new IndexViewModel
            {
                TotalBooks = totalBooks,
                TotalUsers = totalUsers,
                RecentlyAddedBooks = booksToShow,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}