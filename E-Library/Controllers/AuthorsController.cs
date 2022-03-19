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
        public IActionResult Details(string authorId)
        {
            var author = this.authorService.GetAuthor(authorId);
            return View(new AuthorViewModel
            {
                Name = author.Name,
                Description = author.Description,
                ImageUrl = author.ImageUrl
            });
        }
    }
}
