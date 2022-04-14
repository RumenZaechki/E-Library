using E_Library.Models;
using E_Library.Models.Books;
using E_Library.Models.Home;
using E_Library.Services.Books.Models;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace E_Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IMemoryCache cache;
        public HomeController(IHomeService homeService, IMemoryCache cache)
        {
            this.homeService = homeService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestBooksCacheKey = "LatestBooksCacheKey";
            var latestBooks = this.cache.Get<List<BookServiceModel>>(latestBooksCacheKey);

            if (latestBooks == null)
            {
                latestBooks = this.homeService.GetRecentBooks();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                this.cache.Set(latestBooksCacheKey, latestBooks, cacheOptions);
            }

            var booksToShow = latestBooks.Select(b => new BookListingViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                Release = b.Release,
                Author = b.Author,
            })
                .ToList();
            return View(new IndexViewModel
            {
                RecentlyAddedBooks = booksToShow,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string description = null)
        {
            return View(new ErrorViewModel 
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Description = description
            });
        }
    }
}