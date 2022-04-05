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
        public IActionResult All([FromQuery] AllAuthorsQueryModel query)
        {
            var authors = this.authorService
                .GetAuthors(query.CurrentPage, query.AuthorsPerPage, query.SearchTerm)
                .Select(a => new AuthorListingViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                });
            return View(new AllAuthorsQueryModel
            {
                AllAuthors = authors,
                SearchTerm = query.SearchTerm,
                AuthorsCount = this.authorService.GetAuthorsCount(query.SearchTerm),
                CurrentPage = query.CurrentPage,
            });
        }
        public IActionResult Details(string authorId)
        {
            var author = this.authorService.GetAuthor(authorId);
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
