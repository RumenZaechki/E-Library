using E_Library.Models.Authors;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;
        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }
        public async Task<IActionResult> All([FromQuery] AllAuthorsQueryModel query)
        {
            var authors = await this.authorService
                .GetAuthorsAsync(query.CurrentPage, query.AuthorsPerPage, query.SearchTerm);
            var res = authors
                .Select(a => new AuthorListingViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                });
            return View(new AllAuthorsQueryModel
            {
                AllAuthors = res,
                SearchTerm = query.SearchTerm,
                AuthorsCount = await this.authorService.GetAuthorsCountAsync(query.SearchTerm),
                CurrentPage = query.CurrentPage,
            });
        }
        public async Task<IActionResult> Details(string authorId)
        {
            var author = await this.authorService.GetAuthorAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            return View(new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                ImageUrl = author.ImageUrl,
                Books = author.Books
                        .Select(b => new BookViewModel
                        {
                            Id = b.Id,
                            Title = b.Title
                        })
                        .ToList()
            });
        }
    }
}
