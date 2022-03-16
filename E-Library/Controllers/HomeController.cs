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
        private readonly IHomeService homeService;
        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var books = this.homeService.GetBooks();
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
                .ToList();
            return View(new IndexViewModel
            {
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