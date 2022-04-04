using E_Library.Models.Authors;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static E_Library.WebConstants;

namespace E_Library.Areas.Admin.Controllers
{
    public class AuthorsController : AdminController
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public IActionResult Edit(string id)
        {
            var author = authorService.GetAuthor(id);
            return View(new AuthorFormModel
            {
                Name = author.Name,
                Description = author.Description,
                ImageUrl = author.ImageUrl
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(string id, AuthorFormModel author)
        {
            this.authorService.Edit(id, author.Name, author.Description, author.ImageUrl);
            this.TempData[GlobalMessageKey] = "Successfully edited author.";
            return RedirectToAction("Details", "Authors", new { area = "", authorId = id });
        }
    }
}
