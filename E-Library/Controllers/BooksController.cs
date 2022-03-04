using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Models.Books;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext data;
        public BooksController(LibraryDbContext data)
        {
            this.data = data;
        }
        public IActionResult Create() => View(new CreateBookFormModel
        {
            Categories = GetBookCategories()
        });
        [HttpPost]
        public IActionResult Create(CreateBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = GetBookCategories();
                return View(book);
            }
            Author author = null;
            if (!this.data.Authors.Any(a => a.Name == book.Author))
            {
                author = new Author
                {
                    Name = book.Author
                };
                this.data.Authors.Add(author);
                this.data.SaveChanges();
            }
            author = this.data.Authors.Where(a => a.Name == book.Author).FirstOrDefault();
            var bookToAdd = new Book
            {
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                Release = book.Release,
                CategoryId = book.CategoryId,
                AuthorId = author.Id,
            };
            this.data.Books.Add(bookToAdd);
            this.data.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IEnumerable<BookCategoryViewModel> GetBookCategories()
        {
            return this.data
                .Categories
                .Select(c => new BookCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
        }
    }
}
