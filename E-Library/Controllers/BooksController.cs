﻿using E_Library.Models.Books;
using Microsoft.AspNetCore.Mvc;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace E_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult All([FromQuery] AllBooksQueryModel query)
        {
            var categories = this.bookService.GetBookCategories();
            var books = this.bookService
                .FindBooks(query.SearchTerm, query.CurrentPage, AllBooksQueryModel.BooksPerPage)
                .Select(b => new BookListingViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    Release = b.Release,
                    Author = b.Author,
                });
            if (!string.IsNullOrWhiteSpace(query.SelectedCategory))
            {
                books = this.bookService
                    .FindBooksThatMatchCategory(query.SelectedCategory)
                    .Select(b => new BookListingViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        ImageUrl = b.ImageUrl,
                        Release = b.Release,
                        Author = b.Author,
                    });
            }
            return View(new AllBooksQueryModel
            {
                AllBooks = books,
                SearchTerm = query.SearchTerm,
                SelectedCategory = query.SelectedCategory,
                Categories = categories,
                BooksCount = this.bookService.GetBooksCount()
            });
        }

        public IActionResult Create() => View(new BookFormModel
        {
            Categories = GetCategories()
        });

        [HttpPost]
        public IActionResult Create(BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = GetCategories();
                return View(book);
            }
            bookService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.CategoryId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var book = this.bookService.Details(id);
            return View(new BookFormModel
            {
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                Release = book.Release, 
                Author = book.Author,
                Categories = this.bookService
                    .GetBookCategories()
                    .Select(b => new BookCategoryViewModel
                    {
                        Id = b.Key,
                        Name = b.Value
                    })
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(string id, BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = GetCategories();
                return View(book);
            }
            this.bookService.Edit(id, book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author, book.CategoryId);
            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(string id)
        {
            this.bookService.Delete(id);
            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(string id)
        {
            var book = this.bookService.Details(id);
            return View(new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price.ToString("F2"),
                ImageUrl = book.ImageUrl,
                Release = book.Release,
                Author = book.Author,
                Category = book.Category
            });
        }

        private IEnumerable<BookCategoryViewModel> GetCategories()
        {
            var categoriesObj = this.bookService.GetBookCategories();
            return categoriesObj.Select(c => new BookCategoryViewModel
            {
                Id = c.Key,
                Name = c.Value,
            });
        }
    }
}
