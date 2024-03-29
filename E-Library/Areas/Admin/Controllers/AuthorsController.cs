﻿using E_Library.Models.Authors;
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

        public IActionResult Add()
        {
            return View(new AuthorFormModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AuthorFormModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await this.authorService.AddAsync(author.Name, author.Description, author.ImageUrl);
            this.TempData[GlobalMessageKey] = "Successfully added author.";
            return RedirectToAction("All", "Authors", new {area = ""});
        }

        public async Task<IActionResult> Edit(string id)
        {
            var author = await authorService.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(new AuthorFormModel
            {
                Name = author.Name,
                Description = author.Description,
                ImageUrl = author.ImageUrl
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, AuthorFormModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await this.authorService.EditAsync(id, author.Name, author.Description, author.ImageUrl);
            this.TempData[GlobalMessageKey] = "Successfully edited author.";
            return RedirectToAction("Details", "Authors", new { area = "", authorId = id });
        }
    }
}
