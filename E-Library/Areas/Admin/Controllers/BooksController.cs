using E_Library.Models.Books;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static E_Library.WebConstants;


namespace E_Library.Areas.Admin.Controllers
{
    public class BooksController : AdminController
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public async Task<IActionResult> Create()
        {
            return View(new BookFormModel
            {
                Categories = await GetCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = await GetCategories();
                return View(book);
            }
            await bookService.CreateAsync(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.Publisher, book.CategoryId);
            this.TempData[GlobalMessageKey] = "Successfully created book.";
            return RedirectToAction("Index", "Home", new {area = ""});
        }
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var book = await this.bookService.DetailsAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var categories = await this.bookService.GetBookCategoriesAsync();
            return View(new BookFormModel
            {
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                Release = book.Release,
                Author = book.Author,
                Publisher = book.Publisher,
                Categories = categories
                    .Select(b => new BookCategoryViewModel
                    {
                        Id = b.Id,
                        Name = b.Name
                    })
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = await GetCategories();
                return View(book);
            }
            await this.bookService.EditAsync(id, book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.Publisher, book.CategoryId);
            this.TempData[GlobalMessageKey] = "Successfully edited book.";
            return RedirectToAction("All", "Books", new {area = ""});
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await this.bookService.DeleteAsync(id);
            this.TempData[GlobalMessageKey] = "Successfully removed book.";
            return RedirectToAction("All", "Books", new { area = "" });
        }
        private async Task<IEnumerable<BookCategoryViewModel>> GetCategories()
        {
            var categoriesObj = await this.bookService.GetBookCategoriesAsync();
            return categoriesObj.Select(c => new BookCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            });
        }
    }
}